namespace ScanOrganizer.Models;

public enum TagFolderType{
    PrimaryFolder = 1,
    SecondaryFolder = 2
}

public class OcrTag
{
    public int Id { get; set; }
    public string FindTag { get; set; } = "";
    public string FolderName { get; set; } = "";
    public bool IsActive { get; set; } = false;
    public TagFolderType FolderType { get; set; } = TagFolderType.PrimaryFolder;
}