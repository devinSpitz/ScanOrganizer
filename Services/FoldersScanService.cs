using Hangfire;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using WebApplication1.Data;

namespace ScanOrganizer.Services;

public class FolderScanService
{
    private readonly ApplicationDbContext _dbContext;
    
    public FolderScanService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> RemoveExceptions(int id)
    {
        var folder = await _dbContext.FolderScans
            .Include(f => f.Exceptions)
            .FirstOrDefaultAsync(f => f.Id == id);
        
        if (folder == null)
            return false;

        folder.Exceptions.Clear();
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Upsert(FolderScan folderScan,ModelStateDictionary? modelStateDictionary)
    {
        //sortOrder exists
        var parentFolderIsAlreadyMonitored= await _dbContext.FolderScans.FirstOrDefaultAsync(x => x.MonitorFolderPath.Contains(folderScan.MonitorFolderPath) && x.Id != folderScan.Id && x.IsActive);
        if (parentFolderIsAlreadyMonitored != null)
        {
            modelStateDictionary?.AddModelError("MonitorFolderPath", "A parent folder of this folder is already monitored");
            return false;
        }
        
        if (folderScan.Id <= 0)
        {
            await _dbContext.FolderScans.AddAsync(folderScan);
        }
        else
        {
            var old = _dbContext.FolderScans.AsNoTracking().FirstOrDefault(x => x.Id == folderScan.Id);
            if(folderScan.Exceptions.Count <= 0)
                folderScan.Exceptions = old.Exceptions;
            _dbContext.FolderScans.Update(folderScan);
        }

        
        RecurringJob.TriggerJob("MonitorFolderService.MonitorFolders");
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> Remove(int id)
    {
        if (id > 0)
        {
            var tmp = _dbContext.FolderScans.FirstOrDefault(x => x.Id == id);
            if (tmp == null) return true;
            
            _dbContext.FolderScans.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            RecurringJob.TriggerJob("MonitorFolderService.MonitorFolders");
            return true;
        }

        return false;
    }
    
    public IQueryable<FolderScan> GetAll()
    {
        return _dbContext.FolderScans
            .Include(f => f.Exceptions)
            .AsQueryable();
    }

    public async Task<bool> ActivateById(int id)
    {
        var folderScan = await _dbContext.FolderScans.FirstOrDefaultAsync(x => x.Id == id);
        
        if (folderScan == null) return false;

        if (folderScan.IsActive)
            return true;
        
        folderScan.IsActive = true;
        await _dbContext.SaveChangesAsync();
        
        RecurringJob.TriggerJob("MonitorFolderService.MonitorFolders");
        return true;
    }

    public async Task<bool> DeactivateById(int id)
    {
            var folderScan = await _dbContext.FolderScans.FirstOrDefaultAsync(x => x.Id == id);
        
        if (folderScan == null) return false;

        if (!folderScan.IsActive)
            return true;
        
        folderScan.IsActive = false;
        await _dbContext.SaveChangesAsync();
        
        RecurringJob.TriggerJob("MonitorFolderService.MonitorFolders");
        return true;
    }
}