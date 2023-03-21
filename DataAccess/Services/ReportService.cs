using DataAccess.Contexts;
using DataAccess.Models;
using DataAccess.Services.Bases;

namespace DataAccess.Services
{
    public interface IReportService
    {
        List<ReportModel> GetListInnerJoin();
        List<ReportModel> GetListLeftOuterJoin(ReportFilterModel filter);
    }

   
    public class ReportService : IReportService
    {
        private readonly Db _db;

        public ReportService(Db db)
        {
            _db = db;
        }

        public List<ReportModel> GetListInnerJoin()
        {
            var query = from p in _db.Products
                        join c in _db.Categories
                        on p.CategoryId equals c.Id
                        join ps in _db.ProductShops
                        on p.Id equals ps.ProductId
                        join s in _db.Shops
                        on ps.ShopId equals s.Id
                        
                        select new ReportModel()
                        {
                            CategoryName = c.Name,
                            ExpirationDate = p.ExpirationDate,
                            ProductDescription = p.Description,
                            ProductName = p.Name,
                            ProductUnitPrice = p.UnitPrice,
                            ShopName = s.Name,
                            StockAmount = p.StockAmount
                        };
            return query.ToList();
        }

        public List<ReportModel> GetListLeftOuterJoin(ReportFilterModel filter)
        {
            var query = from p in _db.Products
                        join c in _db.Categories
                        on p.CategoryId equals c.Id into categories
                        from subCategory in categories.DefaultIfEmpty()
                        join ps in _db.ProductShops
                        on p.Id equals ps.ProductId into productShops
                        from subProductShop in productShops.DefaultIfEmpty()
                        join s in _db.Shops
                        on subProductShop.ShopId equals s.Id into shops
                        from subShops in shops.DefaultIfEmpty()
                        select new ReportModel()
                        {
                            CategoryName = subCategory.Name,
                            ExpirationDate = p.ExpirationDate,
                            ProductDescription = p.Description,
                            ProductName = p.Name,
                            ProductUnitPrice = p.UnitPrice,
                            ShopName = subShops.Name,
                            StockAmount = p.StockAmount,
                            CategoryId = subCategory.Id,
                            ShopId = subShops.Id,
                            ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : ""
                        };
            query = query.OrderBy(q => q.ShopName).ThenBy(q => q.CategoryName).ThenBy(q => q.ProductName);
            if (filter != null)
            {
                if (filter.CategoryId.HasValue)
                {
                    query = query.Where(q => q.CategoryId == filter.CategoryId);
                }
                if (!string.IsNullOrWhiteSpace(filter.ProductName))
                {
                    query = query.Where(q => q.ProductName.ToLower().StartsWith(filter.ProductName.ToLower()));
                }
                if (filter.ShopIds != null && filter.ShopIds.Count > 0)
                {
                    query = query.Where(q => filter.ShopIds.Contains(q.ShopId ?? 0));
                }
                if (filter.DateStart.HasValue)
                {
                    query = query.Where(q => q.ExpirationDate >= filter.DateStart.Value);
                }
                if (filter.DateEnd.HasValue)
                {
                    query = query.Where(q => q.ExpirationDate <= filter.DateEnd.Value);
                }
            }
            return query.ToList();
        }
    }
}
