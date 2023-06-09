﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;
using NestApp.Models;
using NestApp.Utilities;
using NestApp.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestApp.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]

	public class CategoriesController : Controller
	{
		private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CategoriesController(AppDbContext context, IWebHostEnvironment environment)
        { 
            _context=context;
            _environment=environment;
		}


		public async Task<IActionResult> Index()
		{
			return View(await _context.Categories.ToListAsync());
		}
		public async Task<IActionResult> Detail(int id)
		{
			return View(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
		}
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            if (!category.PhotoFile.CheckFileType("image"))
            {
                ModelState.AddModelError("PhotoFile", "File must be image format");
                return View();
            }
            if (category.PhotoFile.CheckFileSize(200))
            {
                ModelState.AddModelError("PhotoFile", "File must be less than 200kb");
                return View();
            }
            category.Photo = await category.PhotoFile.SaveFileAsync(_environment.WebRootPath, "shop");
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM category)
        {
            Category? exists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (exists == null) return View("error");
            if (category.PhotoFile != null)
            {
                if (!category.PhotoFile.CheckFileType("image"))
                {
                    ModelState.AddModelError("PhotoFile", "File must be image format");
                    return View();
                }
                if (category.PhotoFile.CheckFileSize(200))
                {
                    ModelState.AddModelError("PhotoFile", "File must be less than 200kb");
                    return View();
                }
                string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", exists.Photo);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exists.Photo = await category.PhotoFile.SaveFileAsync(_environment.WebRootPath, "shop");
            }
            exists.Name = category.Name;
            exists.Logo = category.Logo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category? exists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null) return View("Error");

            exists.PhotoFile.DeleteFile(_environment.WebRootPath,
                                        "shop",
                                        exists.Photo);

            exists.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}





        
   
 
