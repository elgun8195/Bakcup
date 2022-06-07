using Fiorella_Webim.DAL;
using Fiorella_Webim.Extensions;
using Fiorella_Webim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorella_Webim.Areas.AdminF.Controllers
{

    [Area("AdminF")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public CategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExistName = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "eyni adda name olmaz");
                return View();
            }
            Category category1 = new Category();
            category1.Name = category.Name;
            category1.Desc = category.Desc;
            await _context.Categories.AddAsync(category1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category dbcategory = await _context.Categories.FindAsync(id);
            if (dbcategory == null) return BadRequest();
            return View(dbcategory);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category dbcategory = await _context.Categories.FindAsync(id);
            Category existNameCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower());
            if (existNameCategory != null)
            {
                if (dbcategory != existNameCategory)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }
            if (dbcategory == null) return BadRequest();
            dbcategory.Name = category.Name;
            dbcategory.Desc = category.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
