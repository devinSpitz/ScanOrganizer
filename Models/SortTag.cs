using System.ComponentModel.DataAnnotations;

namespace ScanOrganizer.Models;

public enum TagFolderType{
    PrimaryFolder = 1,
    SecondaryFolder = 2
}

public class SortTag
{
    public int Id { get; set; }
    public string FindTag { get; set; } = "";
    public string FolderName { get; set; } = "";
    public bool IsActive { get; set; } = false;
    
    /// <summary>
    /// high wins
    /// </summary>
    [Range(0,999)]
    public int SortOrder { get; set; } = 999;
    public TagFolderType FolderType { get; set; } = TagFolderType.PrimaryFolder;
    public bool UseRegex { get; set; } = false;
}