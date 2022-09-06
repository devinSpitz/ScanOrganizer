using System.ComponentModel.DataAnnotations.Schema;

namespace ScanOrganizer.Models;

public delegate void FolderWatchErrorHandler(FolderWatch watch);
public class FolderWatch
{
    public FileSystemWatcher? Watcher;
    public FolderScan Scan;

    private readonly FolderWatchErrorHandler _onErrorHandler;
    
    public FolderWatch(FolderScan scan,FolderWatchErrorHandler errorHandler)
    {
        FileSystemWatcher watcher = new FileSystemWatcher
        {
            Path = scan.MonitorFolderPath,
            NotifyFilter = NotifyFilters.LastWrite,
            IncludeSubdirectories = scan.IncludeSubFolders,
            Filter = "*.*",
            EnableRaisingEvents = true
        };
        watcher.Changed += OnChanged;
        watcher.Error += OnError;
        Watcher = watcher;
        Scan = scan;
        _onErrorHandler = errorHandler;
    }
    
        
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        //Todo Do what you have to do with this file
        
    }
    private void OnError(object source, ErrorEventArgs e)
    {
        //ingore why just tel the master
        Watcher?.Dispose();
        Watcher = null;
        _onErrorHandler.Invoke(this);
    }
}