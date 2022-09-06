using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using ScanOrganizer.Services;
using WebApplication1.Data;

namespace ScanOrganizer.Extensions;

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

            var folderScans = await folderScanService.GetAll().Where(x=>x.IsActive).ToListAsync();
            foreach (var folderScan in folderScans)
            {
                var runningWatch = FolderWatches.FirstOrDefault(x => x.Scan.Id == folderScan.Id );
                if (runningWatch != null)
                {
                    continue; // already running
                }
                FolderWatches.Add(new FolderWatch(folderScan, OnError));
            }
        }
    }

    public void OnError(FolderWatch watch)
    {
        FolderWatches.Remove(watch);
    }
    

}