using AppCore8137.DataAccess.Services;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Services.Bases
{
    public abstract class ProductServiceBase : ServiceBase<Product>
    {
        protected ProductServiceBase(Db dbContext) : base(dbContext)
        {
        }

        public void DeleteImage(int id)
        {
            var product = Query().SingleOrDefault(p => p.Id == id);
            product.Image = null;
            product.ImageExtension = null;
            base.Update(product);
        }
    }
}
