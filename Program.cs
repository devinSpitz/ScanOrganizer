
using Hangfire;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using ScanOrganizer.Extensions;
using ScanOrganizer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddControllersWithViews();
  
//Hangfire
builder.Services.AddHangfire(x => x.UseInMemoryStorage());
builder.Services.AddHangfireServer(x => { x.WorkerCount = 1; });

//Services
builder.Services.AddScoped<OcrTagService>();
builder.Services.AddScoped<FolderScanService>();
builder.Services.AddSingleton<MonitorFolderService>();

var app = builder.Build();
app.UseHangfireDashboard();

//MonitorFolders
var monitorFolderService = app.Services.GetService<MonitorFolderService>();
RecurringJob.AddOrUpdate(
    () => monitorFolderService.MonitorFolders(),
    "*/5 * * * *"); // Check server states

//Migrate db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();     
    
    context.Database.Migrate();
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();