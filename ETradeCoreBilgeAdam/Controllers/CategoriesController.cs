using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;

namespace ETradeCoreBilgeAdam.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        
        private readonly CategoryServiceBase _categoryService;

        public CategoriesController(CategoryServiceBase categoryService)
        {
            _categoryService = categoryService;
        }

        
        public IActionResult Index()
        {
            List<Category> categoryList = _categoryService.GetList(); 
            return View(categoryList);
        }

       
        public IActionResult Details(int id)
        {
            Category category = _categoryService.GetItem(id); 
            if (category == null)
            {
               
                return View("_Error", "Category not found!");
            }
            return View(category);
        }

       
        public IActionResult Create()
        {
            
            var category = new Category();
            return View(category);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int id)
        {
            Category category = _categoryService.GetItem(id);             
            if (category == null)
            {
                return View("_Error", "Category cannot be found!");
            }
       
            return View(category);
        }

        // POST: Categories/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Update(category);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ViewBag.Message = result.Message;
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int id)
        {
            Category category = _categoryService.GetItem(id);
            if (category == null)
            {
                return View("_Error", "Category not found!");
            }
            return View(category);
        }

        // POST: Categories/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _categoryService.Delete(c => c.Id == id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));

        }
	}
}
