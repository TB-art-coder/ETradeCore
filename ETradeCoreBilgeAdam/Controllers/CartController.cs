using DataAccess.Models;
using DataAccess.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ETradeCoreBilgeAdam.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductServiceBase _productService;

        public CartController(ProductServiceBase productService)
        {
            _productService = productService;
        }

        public IActionResult AddToCart(int productId)
        {
            List<CartItemModel> cart = GetSession();

            string json;
            var product = _productService.GetItem(productId);
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            CartItemModel item = new CartItemModel()
            {
                ProductId = productId,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice,
                UserId = userId
            };
            cart.Add(item);
            json = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", json);
            TempData["Message"] = $"{product.Name} added to cart.";
            return RedirectToAction("Index", "Products");
        }

        

        private List<CartItemModel> GetSession(string key = "cart")
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            string json = HttpContext.Session.GetString("cart");
            if (json != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemModel>>(json);
            }
            return cart;
        }

        public IActionResult GetCart()
        {
            List<CartItemModel> cart = GetSession().Select(c => new CartItemModel()
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                UnitPrice = c.UnitPrice,
                UserId = c.UserId,
                UnitPriceDisplay = (c.UnitPrice ?? 0).ToString("C2")
            }).ToList();
            var groupBy = from c in cart
                          group c by new { c.ProductId, c.UserId, c.ProductName }
                          into cGroupBy
                          select new CartItemGroupByModel()
                          {
                              ProductId = cGroupBy.Key.ProductId ?? 0,
                              UserId = cGroupBy.Key.UserId ?? 0,
                              ProductName = cGroupBy.Key.ProductName,
                              TotalPrice = cGroupBy.Sum(cgb => cgb.UnitPrice ?? 0),
                              ProductCount = cGroupBy.Count()
                          };
            return View(groupBy);
        }

        public IActionResult ClearCart()
        {
            
            var cart = GetSession();
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var userCart = cart.Where(c => c.UserId == userId).ToList();

            ;
            cart = cart.Except(userCart).ToList();

            string json = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", json);
            return RedirectToAction(nameof(GetCart));
        }

        public IActionResult DeleteFromCart(int productId)
        {
            var cart = GetSession();
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var cartItem = cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            cart.Remove(cartItem);
            string json = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", json);
            return RedirectToAction(nameof(GetCart));
        }
    }
}
