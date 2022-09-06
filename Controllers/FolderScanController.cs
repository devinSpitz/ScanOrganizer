using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        return View("Index.cshtml", await _folderScanService.GetAll().ToListAsync());
    }
    
    public async Task<IActionResult> Remove(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        var result = await _folderScanService.Remove(id);
        
        if (result)
            return Ok("Folder removed");
        
        return BadRequest("Failed to remove folder");
    }
    
    public async Task<IActionResult> Upsert(FolderScan folderScan)
    {
        if (ModelState.IsValid)
        {
            var result = await _folderScanService.Upsert(folderScan);
            if(result) return await Index();
        }
        return View("Detail.cshtml", folderScan);
    }
    
    public async Task<IActionResult> EditFolder(int? id)
    {
        var folderScan = new FolderScan();
        
        if(id==null)
        {
            return View("Detail.cshtml", folderScan);
        }
        
        folderScan =  await _folderScanService.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        return View("Detail.cshtml", folderScan);
    }
    public async Task<IActionResult> Enable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _folderScanService.ActivateById(id);
        if(result)
            return Ok("Folder activated");
        
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
            return Ok("Folder Deactivated");
        
        return BadRequest("Folder not Deactivated");
    }
}