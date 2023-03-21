using AppCore8137.DataAccess.Results;
using AppCore8137.DataAccess.Results.Bases;
using AppCore8137.DataAccess.Services;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public abstract class UserServiceBase : ServiceBase<User>
    {
        protected UserServiceBase(Db dbContext) : base(dbContext)
        {
        }
    }

    public class UserService : UserServiceBase
    {
        public UserService(Db dbContext) : base(dbContext)
        {
        }

        public override IQueryable<User> Query()
        {
            return base.Query().Include(u => u.Role).Include(u => u.UserDetails);
        }

        public override Result Add(User entity, bool save = true)
        {
            if (Query().Any(u => u.UserName == entity.UserName))
                return new ErrorResult("User with same name exists!");
            if (Query().Any(u => u.UserDetails.Email == entity.UserDetails.Email))
                return new ErrorResult("User with same e-mail exists!");
            return base.Add(entity, save);
        }
    }
}
