using Fiorella_Webim.DAL;
using Fiorella_Webim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorella_Webim.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private AppDbContext _context;

        public ProductsViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = _context.Products.Include(p => p.Category).ToList();
            return View(await Task.FromResult(products));
        }
    }
}
