using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella_Webim.Areas.AdminF.Controllers
{
    public class DashboardController : Controller
    {
        [Area("AdminF")]
        [Authorize(Roles ="Admin,SuperAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
