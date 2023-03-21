using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETradeCoreBilgeAdam.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CityServiceBase _cityServiceBase;

        public CitiesController(CityServiceBase cityServiceBase)
        {
            _cityServiceBase = cityServiceBase;
        }

        public IActionResult GetJson(int countryId) 
        {
            
            var cities = _cityServiceBase.GetList(c => c.CountryId == countryId);
            return Json(cities);
        }
    }
}
