using System.ComponentModel.DataAnnotations.Schema;

namespace ScanOrganizer.Models;

public enum JobType{
    Image2Pdf = 1,
    PdfOcrOrganize = 2
}

public class FolderScan
{
    public int Id { get; set; }
    public string MonitorFolderPath { get; set; } = "";
    public string ResultFolderPath { get; set; } = "";
    public bool IsActive { get; set; } = false;
    public bool IncludeSubFolders { get; set; } = true;
    public JobType FolderType { get; set; } = JobType.PdfOcrOrganize;
    [NotMapped] public FileSystemWatcher Watcher;
}