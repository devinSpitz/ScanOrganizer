using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TesseractSharp;

namespace ScanOrganizer.Models;

public class FolderScan
{
    public int Id { get; set; }
    [DisplayName("Source path")]
    public string MonitorFolderPath { get; set; } = "";
    [DisplayName("Destination path")]
    public string ResultFolderPath { get; set; } = "";
    
    [DisplayName("Enabled")]
    public bool IsActive { get; set; } = false;
    
    [DisplayName("Include subfolders")]
    public bool IncludeSubFolders { get; set; } = true;
    
    [DisplayName("Failures")]
    public List<FolderScanExceptions> Exceptions { get; set; } = new List<FolderScanExceptions>();
    
    [DisplayName("Delete source when finished")]
    public bool DeleteSourceWhenFinished { get; set; } = true;
    
    [NotMapped] public FileSystemWatcher Watcher;
    
    //For pictures to pdf jobs
    
    [DisplayName("Languages")]
    public string Languages { get; set; } = "German";
    
    [NotMapped]
    public List<Language> LanguagesDictionary { get; set; } = new List<Language>();

    
}