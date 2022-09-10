namespace ScanOrganizer.Models;

public class FolderScanExceptions
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string? InnerExceptionMessage { get; set; }

    public FolderScanExceptions(string? innerExceptionMessage, string message)
    {
        this.InnerExceptionMessage = innerExceptionMessage;
        this.Message = message;
    }
}