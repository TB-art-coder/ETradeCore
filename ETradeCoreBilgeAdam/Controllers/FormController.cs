using Microsoft.AspNetCore.Mvc;

namespace ETradeCoreBilgeAdam.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Post(string adi)
        {
            return Content($"Adı: {adi}");
        }
    }
}
