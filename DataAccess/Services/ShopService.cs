using AppCore8137.DataAccess.Results;
using AppCore8137.DataAccess.Results.Bases;
using AppCore8137.DataAccess.Services;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Services
{
    public abstract class ShopServiceBase : ServiceBase<Shop>
    {
        protected ShopServiceBase(Db db) : base(db)
        {

        }
    }

    public class ShopService : ShopServiceBase
    {
        public ShopService(Db db) : base(db)
        {

        }

        public override IQueryable<Shop> Query()
        {
            return base.Query().OrderBy(s => s.Name).Select(s => new Shop()
            {
                Id = s.Id,
                IsVirtual = s.IsVirtual,
                Name = s.Name,
                IsVirtualDisplay = s.IsVirtual ? "Yes" : "No"
            });
        }
    }
}
