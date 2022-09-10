using System.Collections.Concurrent;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Extensions;
using ScanOrganizer.Models;
using ScanOrganizer.Services;
using WebApplication1.Data;

namespace ScanOrganizer.Services;

public class MonitorFolderService
{
    private static List<FolderWatch> FolderWatches = new List<FolderWatch>();
    
    private readonly IServiceScopeFactory _scopeFactory;
        
    public MonitorFolderService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task MonitorFolders()
    {
        using(var scope = _scopeFactory.CreateScope()) 
        {
            var folderScanService = scope.ServiceProvider.GetRequiredService<FolderScanService>();
            
            var folderScans = await folderScanService.GetAll().AsNoTracking().Where(x=>x.IsActive).ToListAsync();
            var folderScansIds = folderScans.Select(x => x.Id).ToArray();
            //Remove all folder watches that are not active or in the database
            foreach (var folderWatch in FolderWatches.Where(folderWatch => !folderScansIds.Contains(folderWatch.Scan.Id)))
            {
                folderWatch.Watcher?.Dispose();
                FolderWatches.Remove(folderWatch);
            }
            
            //Update all folder watches that are active and in the database
            foreach (var folderScan in folderScans.Where(folderScan => FolderWatches.Any(x => x.Scan.Id == folderScan.Id)))
            {
                var folderWatch = FolderWatches.FirstOrDefault(x => x.Scan.Id == folderScan.Id);
                if (folderWatch != null) 
                    folderWatch.Scan = folderScan;
            }
            
            
            //Activate new ones
            foreach (var folderScan in folderScans)
            {
                var runningWatch = FolderWatches.FirstOrDefault(x => x.Scan.Id == folderScan.Id );
                if (runningWatch != null)
                {
                    continue; // already running
                }
                FolderWatches.Add(new FolderWatch(folderScan, OnError,_scopeFactory));
            }
        }
    }

    private void OnError(FolderWatch watch, Exception e)
    {
        RecurringJob.TriggerJob("MonitorFolderService.MonitorFolders");
        watch.Watcher?.Dispose();
        FolderWatches.Remove(watch);
        
        ScanOrganizeHelper.AddExceptionToWatch(watch, e, _scopeFactory);
    }

    
}