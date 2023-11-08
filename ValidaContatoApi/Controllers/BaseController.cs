using Microsoft.AspNetCore.Mvc;
using ValidaContatoApi.Business.Resultados;

namespace ValidaContatoApi.Controllers
{
    [ApiController()]
    [Route("Controller/Api/")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ObterIActionResult<T>(Resultado<T> resultado)
        {
            if (resultado is null)
                return StatusCode(500, null);

            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
