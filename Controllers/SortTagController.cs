using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using ScanOrganizer.Services;

namespace ScanOrganizer.Controllers;

public class SortTagController : Controller
{
    private readonly SortTagService _sortTagService;
    
    public SortTagController(SortTagService sortTagService)
    {
        _sortTagService = sortTagService;
    }
    public async Task<IActionResult> Index()
    {
        return View("Index", await _sortTagService.GetAll().ToArrayAsync());
    }
    
    public async Task<IActionResult> Remove(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        var result = await _sortTagService.Remove(id);

        if (result)
            return await Index();
        
        return BadRequest("Failed to remove SortTag");
    }
    
    public async Task<IActionResult> Upsert(SortTag sortTag)
    {
        if (!ModelState.IsValid) return View("Detail", sortTag);
        
        var result = await _sortTagService.Upsert(sortTag,ModelState);
            
        if(result) return await Index();
        
        return View("Detail", sortTag);
    }
    
    public async Task<IActionResult> EditSortTag(int? id)
    {
        var SortTag = new SortTag();
        
        if(id==null)
        {
            return View("Detail", SortTag);
        }
        
        SortTag =  await _sortTagService.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        return View("Detail", SortTag);
    }
    public async Task<IActionResult> Enable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _sortTagService.ActivateById(id);
        if(result)
            return await Index();
        
        return BadRequest("SortTag not activated");
    }
    
    public async Task<IActionResult> Disable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _sortTagService.DeactivateById(id);
        if(result)
            return await Index();
        
        return BadRequest("SortTag not Deactivated");
    }
}