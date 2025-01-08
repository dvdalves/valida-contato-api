using Microsoft.AspNetCore.Mvc;
using ValidaContatoApi.Business.Common;

namespace ValidaContatoApi.Controllers
{
    [ApiController()]
    [Route("Controller/Api/")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ObterIActionResult<T>(Result<T> resultado)
        {
            if (resultado is null)
                return StatusCode(500, null);

            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
