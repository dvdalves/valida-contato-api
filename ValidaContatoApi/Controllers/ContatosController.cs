using Microsoft.AspNetCore.Mvc;

namespace ValidaContatoApi.Controllers
{
    public class ContatosController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
