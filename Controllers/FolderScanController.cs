using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Extensions;
using ScanOrganizer.Models;
using ScanOrganizer.Services;

namespace ScanOrganizer.Controllers;

public class FolderScanController : Controller
{

    private readonly FolderScanService _folderScanService;
    
    public FolderScanController(FolderScanService folderScanService)
    {
        _folderScanService = folderScanService;
    }
    
    public async Task<IActionResult> Index()
    {
        return View("Index", await _folderScanService.GetAll().ToArrayAsync());
    }
    
    public async Task<IActionResult> Remove(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        var result = await _folderScanService.Remove(id);

        if (result)
            return await Index();
        
        return BadRequest("Failed to remove folder");
    }
    
    public async Task<IActionResult> Upsert(FolderScan folderScan)
    {
        if (!ModelState.IsValid) return View("Detail.cshtml", folderScan);

        folderScan.Languages = "";
        foreach (var selectedLanguages in folderScan.LanguagesDictionary)
        {
               folderScan.Languages += selectedLanguages + ",";
        }
        var result = await _folderScanService.Upsert(folderScan,ModelState);
        if(result) return await Index();
        return View("Detail", folderScan);
    }    
    public async Task<IActionResult> RemoveExceptions(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        var result = await _folderScanService.RemoveExceptions(id);

        if (result)
            return await Index();
        
        return BadRequest("Failed to remove exceptions from ScanFolder");
    }
    
    public async Task<IActionResult> EditFolder(int? id)
    {
        var folderScan = new FolderScan();
        
        if(id==null)
        {
            folderScan.LanguagesDictionary = ScanOrganizeHelper.GetSelectedLanguagesList(folderScan.Languages).ToList();
            return View("Detail", folderScan);
        }
        
        
        folderScan =  await _folderScanService.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        
        folderScan.LanguagesDictionary = ScanOrganizeHelper.GetSelectedLanguagesList(folderScan.Languages).ToList();
        return View("Detail", folderScan);
    }
    public async Task<IActionResult> Enable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _folderScanService.ActivateById(id);
        if(result)
            return await Index();
        
        return BadRequest("Folder not activated");
    }
    
    public async Task<IActionResult> Disable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _folderScanService.DeactivateById(id);
        if(result)
            return await Index();
        
        return BadRequest("Folder not Deactivated");
    }
}