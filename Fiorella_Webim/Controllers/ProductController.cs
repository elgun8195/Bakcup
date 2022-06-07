using Fiorella_Webim.DAL;
using Fiorella_Webim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace Fiorella_Webim.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.ProductCount = _context.Products.Count();
            List<Product> products = _context.Products.Include(p => p.Category).Take(2).ToList();
            return View(products);

        }
        public IActionResult LoadMore(int skip)
        {

            List<Product> products = _context.Products.Include(p => p.Category).Skip(skip).Take(2).ToList();
            return PartialView("_ProductPartial", products);
        }
        public IActionResult Search(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.ToLower().Contains(search)).ToList();
            return PartialView("_Search", products);
        }

    }
}
