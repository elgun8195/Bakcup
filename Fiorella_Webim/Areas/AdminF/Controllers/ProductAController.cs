using Fiorella_Webim.DAL;
using Fiorella_Webim.Models;
using Fiorella_Webim.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiorella_Webim.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class ProductAController : Controller
    {
        private AppDbContext _context;

        public ProductAController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int take = 5, int pagesize = 1)
        {
            List<Product> products = _context.Products.Include(p => p.Category).Skip((pagesize - 1) * take).Take(take).ToList();
            Pagination<ProductVM> pagination = new Pagination<ProductVM>(

                 ReturnPageCount(take),
                 pagesize,
                 MapProductToProductVM(products)
            );


            return View(pagination);
        }
        private List<ProductVM> MapProductToProductVM(List<Product> products)
        {
            List<ProductVM> productsVMs = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Count = p.Count,
                Price = p.Price,
                CategoryName = p.Category.Name
            }).ToList();
            return productsVMs;
        }


        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }
    }
}
