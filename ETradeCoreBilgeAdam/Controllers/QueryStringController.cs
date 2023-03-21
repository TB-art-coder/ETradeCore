using Microsoft.AspNetCore.Mvc;

namespace ETradeCoreBilgeAdam.Controllers
{
    public class QueryStringController : Controller
    {
        public IActionResult Get(string adi, string soyadi, int id)
        {
            return Content($"Adı: {adi} {soyadi}, Id: {id}");
        }
    }
}
