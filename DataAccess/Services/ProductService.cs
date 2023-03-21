using AppCore8137.DataAccess.Results;
using AppCore8137.DataAccess.Results.Bases;
using AppCore8137.Utils;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace DataAccess.Services
{
    public class ProductService : ProductServiceBase
    {
        public ProductService(Db dbContext) : base(dbContext)
        {
           
        }

        public override IQueryable<Product> Query()
        {
            return base.Query().Include(p => p.Category).Include(p => p.ProductShops)
                
                .OrderBy(p => p.UnitPrice).ThenByDescending(p => p.Name).Select(p => new Product() 
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                Description = p.Description,
                ExpirationDate = p.ExpirationDate,
                UnitPrice = p.UnitPrice,
                StockAmount = p.StockAmount,
                CategoryNameDisplay = p.Category.Name,
               ",
                UnitPriceDisplay = p.UnitPrice != null ? p.UnitPrice.Value.ToString("C2") : "", 
                ProductShops = p.ProductShops,
                ShopNamesDisplay = string.Join("<br />", p.ProductShops.Select(ps => ps.Shop.Name)),
                ShopIds = p.ProductShops.Select(ps => ps.ShopId ?? 0).ToList(),
                Image = p.Image,
                ImageExtension = p.ImageExtension,
                ImageTagSrcDisplay = p.Image != null ? FileUtil.GetContentType(p.ImageExtension, true, true) + Convert.ToBase64String(p.Image) : null
            });
        }

        public override Result Add(Product entity, bool save = true)
        {
            
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim())) 
                return new ErrorResult("The name you entered exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();

            entity.ProductShops = entity.ShopIds?.Select(sId => new ProductShop()
            {
                ShopId = sId
            }).ToList();

            var result = base.Add(entity, save); 
            if (result.IsSuccessful)
                result.Message = "Product added succesfully.";
            return result;
        }

        public override Result Update(Product entity, bool save = true)
        {
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim() && p.Id != entity.Id))
                return new ErrorResult("The name you entered exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();

            
            var product = Query().SingleOrDefault(p => p.Id == entity.Id);
            _dbContext.Set<ProductShop>().RemoveRange(product.ProductShops);

            entity.ProductShops = entity.ShopIds?.Select(sId => new ProductShop()
            {
                ShopId = sId
            }).ToList();

            if (entity.Image == null)
            {
                entity.Image = product.Image;
                entity.ImageExtension = product.ImageExtension;
            }

            return base.Update(entity, save);
        }

        public override Result Delete(Expression<Func<Product, bool>> predicate, bool save = true)
        {
            
            var product = Query().SingleOrDefault(predicate);
            _dbContext.Set<ProductShop>().RemoveRange(product.ProductShops);
            return base.Delete(predicate, save);
        }

       
    }
}
