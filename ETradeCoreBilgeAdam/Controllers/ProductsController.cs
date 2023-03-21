using AppCore8137.Utils;
using DataAccess.Entities;
using DataAccess.Services;
using DataAccess.Services.Bases;
using ETradeCoreBilgeAdam.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETradeCoreBilgeAdam.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        // Add service injections here
        private readonly ProductServiceBase _productService;
        private readonly CategoryServiceBase _categoryService;
        private readonly ShopServiceBase _shopService;

        public ProductsController(ProductServiceBase productService,
            CategoryServiceBase categoryService, ShopServiceBase shopService
            )
        {
            _productService = productService;
            _categoryService = categoryService;
            _shopService = shopService;
        }

        // GET: Products
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Product> productList = _productService.GetList();
            //ViewData["Count"] = productList.Count == 0 ? "No records found." : productList.Count == 1 ? "1 record found." : productList.Count + " records found.";
            ViewBag.Count = productList.Count == 0 ? "No records found." : productList.Count == 1 ? "1 record found." : productList.Count + " records found.";
            return View(productList);
        }

        // GET: Products/Details/5
        [Authorize(Roles = "admin")]
        public IActionResult Details(int id)
        {
            Product product = _productService.GetItem(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        //[HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            var categories = _categoryService.GetList();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewBag.ShopIds = new MultiSelectList(_shopService.GetList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Product product, IFormFile? productImage)
        {
            if (ModelState.IsValid)
            {
                var updateResult = UpdateImage(product, productImage); // ***
                if (updateResult == false)
                {
                    ModelState.AddModelError("", "File extension and length are not valid!");
                }
                else
                {
                    var result = _productService.Add(product);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                        //return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result.Message); // validation summary
                }
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            ViewBag.ShopIds = new MultiSelectList(_shopService.GetList(), "Id", "Name", product.ShopIds);
            return View(product);
        }

        private bool? UpdateImage(Product entity, IFormFile uploadedFile)
        {
            #region Validation
            bool? result = null;
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                result = FileUtil.CheckFileExtension(uploadedFile.FileName, AppSettings.FileExtensions).IsSuccessful; // ***
                if (result == true)
                {
                    result = FileUtil.CheckFileLength(uploadedFile.Length, AppSettings.FileLength).IsSuccessful; // ***
                }
            }
            #endregion

            #region Dosyanın Kaydedilmesi
            if (result == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    uploadedFile.CopyTo(memoryStream);
                    entity.Image = memoryStream.ToArray();
                    entity.ImageExtension = Path.GetExtension(uploadedFile.FileName);
                }
            }
            #endregion

            return result;
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Product product = _productService.GetItem(id);
            if (product == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewBag.ShopIds = new MultiSelectList(_shopService.GetList(), "Id", "Name", product.ShopIds);
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Product product, IFormFile? productImage)
        {
            if (ModelState.IsValid)
            {
                var updateResult = UpdateImage(product, productImage); // ***
                if (updateResult == false)
                {
                    ModelState.AddModelError("", "File extension and length are not valid!");
                }
                else
                {
                    var result = _productService.Update(product);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", result.Message);
                }
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            ViewBag.ShopIds = new MultiSelectList(_shopService.GetList(), "Id", "Name", product.ShopIds);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(p => p.Id == id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteImage(int id)
        {
            _productService.DeleteImage(id);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
