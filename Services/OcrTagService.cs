using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using WebApplication1.Data;

namespace ScanOrganizer.Services;

public class OcrTagService
{
    private readonly ApplicationDbContext _dbContext;
    
    public OcrTagService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Upsert(OcrTag ocrTag)
    {
        //Unique key
        var exists= await _dbContext.OcrTags.FirstOrDefaultAsync(x => x.FindTag == ocrTag.FindTag);
        if (exists != null && ocrTag.Id != exists.Id)
        {
            return false;
        }
        
        if (ocrTag.Id > 0)
        {
            await _dbContext.OcrTags.AddAsync(ocrTag);
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Remove(int id)
    {
        if (id > 0)
        {
            var tmp = _dbContext.OcrTags.FirstOrDefault(x => x.Id == id);
            if (tmp == null) return true;
            
            _dbContext.OcrTags.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    
    
    public IQueryable<OcrTag> GetAll()
    {
        return _dbContext.OcrTags.AsQueryable();
    }

    public async Task<bool> ActivateById(int id)
    {
        var ocrTag = await _dbContext.OcrTags.FirstOrDefaultAsync(x => x.Id == id);
        
        if (ocrTag == null) return false;

        if (ocrTag.IsActive)
            return true;
        
        ocrTag.IsActive = true;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateById(int id)
    {
        var ocrTag = await _dbContext.OcrTags.FirstOrDefaultAsync(x => x.Id == id);
        
        if (ocrTag == null) return false;

        if (!ocrTag.IsActive)
            return true;
        
        ocrTag.IsActive = false;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}