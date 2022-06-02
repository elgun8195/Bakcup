using Fiorella_Webim.DAL;
using Fiorella_Webim.Extensions;
using Fiorella_Webim.Helpers;
using Fiorella_Webim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorella_Webim.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _context.Sliders.ToList();
            return View(sliderList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!slider.Photo.isImage())
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            if (slider.Photo.CheckSize(1000))
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            string filename = await slider.Photo.SaveImage(_webhost, "img");
            Slider newslider = new Slider();
            newslider.ImageUrl = filename;
            await _context.Sliders.AddAsync(newslider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(slider);
            Helper.DeleteFile(_webhost, "img", slider.ImageUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Updatem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbslider = await _context.Sliders.FindAsync(id);
            if (dbslider == null) return BadRequest();
            return View(dbslider);
        }
        [HttpPost]
        public async Task<IActionResult> Updatem(int? id, Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Slider dbslider = await _context.Sliders.FindAsync(id);
            Slider existNameCategory = _context.Sliders.FirstOrDefault(c => c.Photo.FileName.ToLower() == slider.Photo.FileName.ToLower());
            if (existNameCategory != null)
            {
                if (dbslider != existNameCategory)
                {
                    ModelState.AddModelError("FileName", "FileName Already Exist");
                    return View();
                }
            }
            if (dbslider == null) return BadRequest();
            dbslider.Photo = slider.Photo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}
