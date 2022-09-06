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

    public async Task<bool> Upsert(FolderScan folderScan)
    {
        if (folderScan.Id > 0)
        {
            await _dbContext.FolderScans.AddAsync(folderScan);
        }
        else
        {
            _dbContext.FolderScans.Update(folderScan);
        }

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
            return true;
        }

        return false;
    }
    
    public IQueryable<FolderScan> GetAll()
    {
        return _dbContext.FolderScans.AsQueryable();
    }

    public async Task<bool> ActivateById(int id)
    {
        var folderScan = await _dbContext.FolderScans.FirstOrDefaultAsync(x => x.Id == id);
        
        if (folderScan == null) return false;

        if (folderScan.IsActive)
            return true;
        
        folderScan.IsActive = true;
        await _dbContext.SaveChangesAsync();
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
        return true;
    }
}