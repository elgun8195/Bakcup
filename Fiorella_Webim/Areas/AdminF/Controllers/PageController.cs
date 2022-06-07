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
    public class PageController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public PageController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<PageIntro> pageIntro = _context.PageIntros.ToList();
            return View(pageIntro);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PageIntro pageIntro = await _context.PageIntros.FindAsync(id);
            if (pageIntro == null)
            {
                return NotFound();
            }
            return View(pageIntro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PageIntro page = await _context.PageIntros.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            _context.PageIntros.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAll()
        {
            List<PageIntro> pageIntros = _context.PageIntros.ToList();
            foreach (var item in pageIntros)
            {

                if (pageIntros == null)
                {
                    return NotFound();
                }
                _context.PageIntros.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PageIntro dbpageIntro = await _context.PageIntros.FindAsync(id);
            if (dbpageIntro == null) return BadRequest();
            return View(dbpageIntro);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, PageIntro pageIntro)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            PageIntro dbpageIntro = await _context.PageIntros.FindAsync(id);
            PageIntro existNamePageintro = _context.PageIntros.FirstOrDefault(c => c.Title.ToLower() == pageIntro.Title.ToLower());
            if (existNamePageintro != null)
            {
                if (dbpageIntro != existNamePageintro)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }
            if (dbpageIntro == null) return BadRequest();
            dbpageIntro.Title = pageIntro.Title;
            dbpageIntro.Desc = pageIntro.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageIntro page)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!page.Photo.isImage())
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            if (page.Photo.CheckSize(1000))
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            string filename = await page.Photo.SaveImage(_webhost, "img");
            PageIntro page1 = new PageIntro();
            page1.Desc=page.Desc;
            page1.Title = page.Title;
            page1.ImageUrl = filename;
            await _context.PageIntros.AddAsync(page1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}