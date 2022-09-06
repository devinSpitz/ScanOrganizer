using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<OcrTag> OcrTags { get; set; }
    public DbSet<FolderScan> FolderScans { get; set; }
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}