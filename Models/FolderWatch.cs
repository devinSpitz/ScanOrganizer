using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PdfiumViewer;
using ScanOrganizer.Extensions;
using ScanOrganizer.Services;
using TesseractSharp;

namespace ScanOrganizer.Models;

public delegate void FolderWatchErrorHandler(FolderWatch watch, Exception ex);
public class FolderWatch
{
    public FileSystemWatcher? Watcher;
    public FolderScan Scan;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly FolderWatchErrorHandler _onErrorHandler;
    
    public FolderWatch(FolderScan scan,FolderWatchErrorHandler errorHandler,IServiceScopeFactory scopeFactory)
    {
        FileSystemWatcher watcher = new FileSystemWatcher
        {
            Path = scan.MonitorFolderPath,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
            IncludeSubdirectories = scan.IncludeSubFolders,
            Filter = "*.*",
            EnableRaisingEvents = true,

        };
        _scopeFactory = scopeFactory;
        watcher.Created += OnCreated;
        watcher.Renamed += OnCreated;
        watcher.Error += OnError;
        Watcher = watcher;
        Scan = scan;
        _onErrorHandler = errorHandler;
        
    }
    
        
    private void OnCreated(object source, FileSystemEventArgs e)
    {
        var changesToReact = new[]
        {
            WatcherChangeTypes.Created,
            WatcherChangeTypes.Renamed
        };

        if (!changesToReact.Contains(e.ChangeType)) return;

        try
        {
            ImageToPdfConvertion(e.FullPath).GetAwaiter().GetResult();
        }
        catch (Exception exception)
        {
            ScanOrganizeHelper.AddExceptionToWatch(this,exception,_scopeFactory);
        }
    }

    private bool SavePdfAndDeleteIfNeeded(string eFullPath, PdfDocument pdf, string resultFolder)
    {
        var fileName = Scan.ResultFolderPath + resultFolder + Path.GetFileNameWithoutExtension(eFullPath) + Guid.NewGuid() +".pdf";
        if (File.Exists(fileName))
        {
            ScanOrganizeHelper.AddExceptionToWatch(this,new Exception("File already exists"),_scopeFactory);
            
            pdf.Dispose();
            return false;
        }
        
        pdf.Save(fileName);
        pdf.Dispose();

        if (Scan.DeleteSourceWhenFinished)
        {
            File.Delete(eFullPath);
            if (File.Exists(eFullPath))
            {
                ScanOrganizeHelper.AddExceptionToWatch(this,new Exception("Source file could not be deleted"),_scopeFactory);
                return false;
            }
        }
        
        if (File.Exists(fileName))
            return true;

        return false;
    }
    
    private async Task<bool> PdfOcrOrganizeJob(string eFullPath,Stream pdfStream,string text)
    {
        //Convert pdf into ocr via tesseract
        
        var pdf = PdfDocument.Load(pdfStream);
        
        return await PdfOcrOrganizeJob(eFullPath, pdf, text);
    }

    private async Task<bool> PdfOcrOrganizeJob(string eFullPath,PdfDocument pdf,string text)
    {
        var resultFolder = await ResultFolderFromText(text);
        
        return SavePdfAndDeleteIfNeeded(eFullPath, pdf, resultFolder);
    }

    private async Task<string> ResultFolderFromText(string text)
    {
        //does not work without that line dont know why

        var resultFolder = "";
        using (var scope = _scopeFactory.CreateScope())
        {
            var sortTagService = scope.ServiceProvider.GetRequiredService<SortTagService>();
            // for eacha ctive tag search in the text
            foreach (var tag in await sortTagService.GetAll()
                         .Where(x => x.IsActive && x.FolderType == TagFolderType.PrimaryFolder).OrderBy(x => x.SortOrder)
                         .ToArrayAsync())
            {
                resultFolder = GetValue(tag, text, resultFolder);
                if (resultFolder == "") continue;
                CreateFolderIfNotExists(resultFolder);
                break;
            }

            var beforeFolder = resultFolder;
            foreach (var tag in await sortTagService.GetAll()
                         .Where(x => x.IsActive && x.FolderType == TagFolderType.SecondaryFolder).OrderBy(x => x.SortOrder)
                         .ToArrayAsync())
            {
                resultFolder = GetValue(tag, text, resultFolder);
                if (resultFolder == beforeFolder) continue;
                CreateFolderIfNotExists(resultFolder);
                break;
            }
        }

        //write stream as pdf to the result folder
        if (resultFolder == "")
        {
            resultFolder = "/Unsorted/";
            CreateFolderIfNotExists(resultFolder);
        }
        
        
        return resultFolder;
    }

    private void CreateFolderIfNotExists(string resultFolder)
    {
        if (!Directory.Exists(Scan.ResultFolderPath + resultFolder))
        {
            Directory.CreateDirectory(Scan.ResultFolderPath + resultFolder);
        }
    }

    private string GetValue(SortTag tag, string text, string resultFolder)
    {
        if (tag.UseRegex)
        {
            var regex = new Regex(tag.FindTag);

            if (regex.IsMatch(text))
            {
                resultFolder += "/" + tag.FolderName + "/";
                return resultFolder;
            }
        }
        else
        {
            var toCheck = tag.FindTag;
            var textToUse = text;
            
            if (tag.CaseInsensitive)
            {
                toCheck = toCheck.ToLower();
                textToUse = textToUse.ToLower();
            }
            
            if (textToUse.Contains(toCheck))
            {
                resultFolder += "/" + tag.FolderName + "/";
                return resultFolder;
            }
            
        }

        return resultFolder;
    }
    
    public async Task ImageToPdfConvertion(string eFullPath)
    {
        var languages = ScanOrganizeHelper.GetSelectedLanguagesList(Scan.Languages);

        var neededPermissions = FileAccess.Read;
        if (!IsItAllowed(eFullPath, neededPermissions,"File can not be read!"))
        {
            return;
        }

        if (Scan.DeleteSourceWhenFinished)
        {
            neededPermissions |= FileAccess.Write;
            
            if (!IsItAllowed(eFullPath, neededPermissions,"File can not be written to!"))
            {
                return;
            }
        }

        var text = "";
        //check if this file is an image
        if (IsPdf(eFullPath))
        {
            var pdf = PdfDocument.Load(eFullPath);
            var pages = pdf.PageCount;
            for (int i = 0; i < pages; i++)
            {
               text += pdf.GetPdfText(i)+Environment.NewLine; 
            }
            //extract text from pdf
            PdfOcrOrganizeJob(eFullPath,pdf,text).GetAwaiter().GetResult();
            return;
            
        }
        if (!IsImage(eFullPath)) return;
        
        var done = false;
        //Only get text
        var bitmap = (Bitmap)Image.FromFile(eFullPath);
        
        await using(var stream = Tesseract.ImageToTxt(bitmap, languages: languages))
        {
            var reader = new StreamReader(stream);
            //write pdf to the result folder
            text = await reader.ReadToEndAsync();
            reader.Close();
            stream.Close();
        }
        
        await using(var stream = Tesseract.ImageToPdf(bitmap, languages: languages))
        {
            //write pdf to the result folder
            done = await PdfOcrOrganizeJob(eFullPath,stream,text);
            stream.Close();
        }
        
        bitmap.Dispose();
    }

    private bool IsItAllowed(string eFullPath, FileAccess neededPermissions,string error)
    {
        if (ScanOrganizeHelper.IsFileLocked(eFullPath, neededPermissions))
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
            if (ScanOrganizeHelper.IsFileLocked(eFullPath, neededPermissions))
            {
                ScanOrganizeHelper.AddExceptionToWatch(this, new Exception(error), _scopeFactory);
                return false;
            }

            return true;
        }

        return true;
    }


    private bool IsImage(string eFullPath)
    {
        //check if file is an image
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(eFullPath);
        if(!imageExtensions.Contains(extension)) return false;
        
        //read file header to check that file is an image
        var header = new byte[4];
        using (var stream = new FileStream(eFullPath, FileMode.Open))
        {
            stream.Read(header, 0, header.Length);
            stream.Close();
        }

        var bmp = Encoding.ASCII.GetString(header);
        if (bmp == "BM") return true; // BMP
        if (bmp == "GIF") return true; // GIF
        if (bmp == Encoding.ASCII.GetString(new byte[] {137, 80, 78, 71})) return true; // PNG
        if (bmp == Encoding.ASCII.GetString(new byte[] {73, 73, 42})) return true; // TIFF
        if (bmp == Encoding.ASCII.GetString(new byte[] {77, 77, 42})) return true; // TIFF
        if (bmp == Encoding.ASCII.GetString(new byte[] {255, 216, 255, 224})) return true; // jpeg
        if (bmp == Encoding.ASCII.GetString(new byte[] {255, 216, 255, 225})) return true; // jpeg canon
        
        return false;
    }
   
    private bool IsPdf(string eFullPath)
    {
        //check if file is an image
        var imageExtensions = new[] { ".pdf" };
        var extension = Path.GetExtension(eFullPath);
        if(!imageExtensions.Contains(extension)) return false;
        
        //read file header to check that file is an image
        var header = new byte[4];
        using (var stream = new FileStream(eFullPath, FileMode.Open))
        {
            stream.Read(header, 0, header.Length);
            stream.Close();
        }

        var bmp = Encoding.ASCII.GetString(header);
        if (bmp == Encoding.ASCII.GetString(new byte[] {37, 80, 68, 70})) return true; // pdf
        
        return false;
    }

    private void OnError(object source, ErrorEventArgs e)
    {
        _onErrorHandler.Invoke(this,e.GetException());
    }
}