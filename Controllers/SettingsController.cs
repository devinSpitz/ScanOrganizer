using Microsoft.AspNetCore.Mvc;

namespace ScanOrganizer.Controllers;

public class SettingsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}