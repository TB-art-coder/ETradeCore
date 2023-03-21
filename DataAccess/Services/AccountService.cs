using AppCore8137.DataAccess.Results.Bases;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserServiceBase _userService;

        public AccountService(UserServiceBase userService)
        {
            _userService = userService;
        }

        public User Login(AccountLoginModel model)
        {
            var user = _userService.Query().SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password && u.IsActive);
            return user;
        }

        public Result Register(AccountRegisterModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Password = model.Password,
                UserDetails = new UserDetails()
                {
                    Address = model.Address.Trim(),
                    CityId = model.CityId,
                    CountryId = model.CountryId,
                    Email = model.Email.Trim(),
                    Sex = model.Sex
                },
                IsActive = true,
                RoleId = (int)Roles.User
            };
            return _userService.Add(user);
        }
    }
}
