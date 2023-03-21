using AppCore8137.DataAccess.Services;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public abstract class CityServiceBase : ServiceBase<City>
    {
        protected CityServiceBase(Db dbContext) : base(dbContext)
        {
        }
    }

    public class CityService : CityServiceBase
    {
        public CityService(Db dbContext) : base(dbContext)
        {
        }

        public override IQueryable<City> Query()
        {
            return base.Query().Include(c => c.Country).OrderBy(c => c.Name);
        }
    }
}
