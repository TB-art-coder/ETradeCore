using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ProductDemoService 
    {
        private readonly Db _db;

        public ProductDemoService(Db db)
        {
            _db = db;
        }

        public IQueryable<ProductDemoModel> Query()
        {
            return _db.Products.Include("Category").Select(product => new ProductDemoModel()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                ExpirationDate = product.ExpirationDate,
                StockAmount = product.StockAmount,
                UnitPrice = product.UnitPrice,

                UnitPriceDisplay = product.UnitPrice.HasValue ? product.UnitPrice.Value.ToString("C2", new CultureInfo("en-US")) : "", // (product.UnitPrice != null),
                ExpirationDateDisplay = product.ExpirationDate.HasValue ? product.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                CategoryDisplay = product.Category.Name
            });
        }
    }
}
