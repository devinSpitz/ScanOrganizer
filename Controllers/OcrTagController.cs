using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using ScanOrganizer.Services;

namespace ScanOrganizer.Controllers;

public class OcrTagController : Controller
{
    private readonly OcrTagService _ocrTagService;
    
    public OcrTagController(OcrTagService ocrTagService)
    {
        _ocrTagService = ocrTagService;
    }
    public async Task<IActionResult> Index()
    {
        return View("Index.cshtml", await _ocrTagService.GetAll().ToListAsync());
    }
    
    public async Task<IActionResult> Remove(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        var result = await _ocrTagService.Remove(id);
        
        if (result)
            return Ok("ocrTag removed");
        
        return BadRequest("Failed to remove ocrTag");
    }
    
    public async Task<IActionResult> Upsert(OcrTag OcrTag)
    {
        if (ModelState.IsValid)
        {
            var result = await _ocrTagService.Upsert(OcrTag);
            if(result) return await Index();
        }
        return View("Detail.cshtml", OcrTag);
    }
    
    public async Task<IActionResult> EditocrTag(int? id)
    {
        var OcrTag = new OcrTag();
        
        if(id==null)
        {
            return View("Detail.cshtml", OcrTag);
        }
        
        OcrTag =  await _ocrTagService.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        return View("Detail.cshtml", OcrTag);
    }
    public async Task<IActionResult> Enable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _ocrTagService.ActivateById(id);
        if(result)
            return Ok("ocrTag activated");
        
        return BadRequest("ocrTag not activated");
    }
    
    public async Task<IActionResult> Disable(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid id");
        }
        
        var result = await _ocrTagService.DeactivateById(id);
        if(result)
            return Ok("ocrTag Deactivated");
        
        return BadRequest("ocrTag not Deactivated");
    }
}