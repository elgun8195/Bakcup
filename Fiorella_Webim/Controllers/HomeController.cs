using Fiorella_Webim.DAL;
using Fiorella_Webim.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fiorella_Webim.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                sliders = _context.Sliders.ToList(),
                pageIntro = _context.PageIntros.FirstOrDefault(),
                categories = _context.Categories.ToList()
            };


            return View(homeVM);
        }
    }
}
