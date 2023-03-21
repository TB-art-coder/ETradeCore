using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
    using DataAccess.Contexts;
    using DataAccess.Entities;
using DataAccess.Services.Bases;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Add service injections here
        private readonly ProductServiceBase _productService;

        public ProductsController(ProductServiceBase productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> productList = _productService.GetList(); // TODO: Add get list service logic here
            return Ok(productList);
        }

        // GET: api/Products/Details/5
        [HttpGet("Details/{id?}")]
        public IActionResult Details(int? id)
        {
            Product product = _productService.GetItem(id ?? 0); // TODO: Add get item service logic here
			if (product == null)
            {
                return NotFound(); // 404
            }
			return Ok(product); // 200
        }

		// POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(Product product)
        {

            var result = _productService.Add(product);
            if (result.IsSuccessful)
            {
                //return CreatedAtAction("Get", new { id = product.Id }, product);
                return Ok(product);
            }
            ModelState.AddModelError("", result.Message);
            return BadRequest(ModelState);  // 400
        }

        // PUT: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(Product product)
        {
            var result = _productService.Update(product);
            if (result.IsSuccessful)
            {
                //return CreatedAtAction("Get", new { id = product.Id }, product);
                //return Ok(product);
                return NoContent();
            }
            ModelState.AddModelError("", result.Message);
            return BadRequest(ModelState);  // 400
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(p => p.Id == id);
            return Ok(id);
        }
	}
}
