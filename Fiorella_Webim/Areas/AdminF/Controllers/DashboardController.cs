using Microsoft.AspNetCore.Mvc;

namespace Fiorella_Webim.Areas.AdminF.Controllers
{
    public class DashboardController : Controller
    {
        [Area("AdminF")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
