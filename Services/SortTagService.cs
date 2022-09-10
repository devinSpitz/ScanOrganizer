using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;
using WebApplication1.Data;

namespace ScanOrganizer.Services;

public class SortTagService
{
    private readonly ApplicationDbContext _dbContext;
    
    public SortTagService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Upsert(SortTag sortTag,ModelStateDictionary modelStateDictionary)
    {
        //Unique key
        var exists= await _dbContext.SortTags.FirstOrDefaultAsync(x => x.FindTag == sortTag.FindTag && x.Id != sortTag.Id);
        if (exists != null)
        {
            modelStateDictionary.AddModelError("FindTag", "Tag already exists");
            return false;
        }
        //sortOrder exists
        if (sortTag.SortOrder != 999) //default value
        {
            var sortIdExists= await _dbContext.SortTags.FirstOrDefaultAsync(x => x.SortOrder == sortTag.SortOrder && x.Id != sortTag.Id);
            if (sortIdExists != null)
            {
                modelStateDictionary.AddModelError("SortOrder", "SortOrder already exists");
                return false;
            }
        }
        
        if (sortTag.Id <= 0)
        {
            await _dbContext.SortTags.AddAsync(sortTag);
        }
        else
        {
            _dbContext.SortTags.Update(sortTag);
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Remove(int id)
    {
        if (id > 0)
        {
            var tmp = _dbContext.SortTags.FirstOrDefault(x => x.Id == id);
            if (tmp == null) return true;
            
            _dbContext.SortTags.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    
    
    public IQueryable<SortTag> GetAll()
    {
        return _dbContext.SortTags.OrderBy(x=>x.SortOrder).AsQueryable();
    }

    public async Task<bool> ActivateById(int id)
    {
        var sortTag = await _dbContext.SortTags.FirstOrDefaultAsync(x => x.Id == id);
        
        if (sortTag == null) return false;

        if (sortTag.IsActive)
            return true;
        
        sortTag.IsActive = true;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateById(int id)
    {
        var sortTag = await _dbContext.SortTags.FirstOrDefaultAsync(x => x.Id == id);
        
        if (sortTag == null) return false;

        if (!sortTag.IsActive)
            return true;
        
        sortTag.IsActive = false;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}