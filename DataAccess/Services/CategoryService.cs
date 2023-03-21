using AppCore8137.DataAccess.Results;
using AppCore8137.DataAccess.Results.Bases;
using AppCore8137.DataAccess.Services;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Services
{
    public abstract class CategoryServiceBase : ServiceBase<Category>
    {
        protected CategoryServiceBase(Db db) : base(db)
        {

        }
    }

    public class CategoryService : CategoryServiceBase
    {
        public CategoryService(Db db) : base(db)
        {

        }

        public override IQueryable<Category> Query()
        {
            return base.Query().Include(c => c.Products).OrderBy(q => q.Name).Select(c => new Category()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ProductCountDisplay = c.Products == null ? 0 : c.Products.Count,
                Products = c.Products
            });
        }

        public override Result Add(Category entity, bool save = true)
        {
            if (Query().Any(c => c.Name.ToUpper() == entity.Name.ToUpper().Trim()))
                return new ErrorResult("Category with same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            return base.Add(entity, save);
        }

        public override Result Update(Category entity, bool save = true)
        {
            if (Query().Any(c => c.Name.ToUpper() == entity.Name.ToUpper().Trim() && c.Id != entity.Id))
                return new ErrorResult("Category with same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            return base.Update(entity, save);
        }

        public override Result Delete(Expression<Func<Category, bool>> predicate, bool save = true)
        {
            var category = Query().SingleOrDefault(predicate);
            if (category.Products != null && category.Products.Count > 0)
                return new ErrorResult("Category cannot be deleted, it has products!");
            return base.Delete(predicate, save);
        }
    }
}
