using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using ScanOrganizer.Services;
using TesseractSharp;

namespace ScanOrganizer.Extensions;

public static class ScanOrganizeHelper
{
    
    public static Language[] GetSelectedLanguagesList(string languagesString)
    {
        if(languagesString.EndsWith(","))
            languagesString = languagesString.Remove(languagesString.Length - 1);
        var languagesSingle = languagesString.Split(",");
        var languagesCount = languagesString.Split(",").Length;

        var languages = new Language[languagesCount];
        for (var i = 0; i < languagesCount; i++)
        {
            languages[i] = (Language) Enum.Parse(typeof(Language), languagesSingle[i]);
        }

        return languages;
    }
    
    public static void AddExceptionToWatch(FolderWatch watch, Exception e,IServiceScopeFactory scopeFactory)
    {
        using (var scope = scopeFactory.CreateScope())
        {
            var folderScanService = scope.ServiceProvider.GetRequiredService<FolderScanService>();
            var scan = folderScanService.GetAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == watch.Scan.Id)
                .GetAwaiter()
                .GetResult();
            if (scan == null) return;
            scan.Exceptions.Add(new FolderScanExceptions(e.InnerException?.Message, e.Message));
            folderScanService.Upsert(scan, null).GetAwaiter().GetResult();
        }
    }

    public static bool IsFileLocked(string eFullPath,FileAccess fileAccess)
    {
        try
        {
            using var stream = File.Open(eFullPath, FileMode.Open, fileAccess , FileShare.None);
            stream.Dispose();
            return false;
        }
        catch (IOException)
        {
            return true;
        }
    }
}