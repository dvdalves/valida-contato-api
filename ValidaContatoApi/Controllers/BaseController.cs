using Microsoft.AspNetCore.Mvc;

namespace ValidaContatoApi.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
