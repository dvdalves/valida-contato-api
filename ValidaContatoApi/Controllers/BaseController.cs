using Microsoft.AspNetCore.Mvc;
using ValidaContatoApi.Business.Common;

namespace ValidaContatoApi.Controllers;

[ApiController()]
[Route("api/")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult GetActionResult<T>(Result<T> result)
    {
        if (result is null)
            return StatusCode(500, null);

        return StatusCode(result.StatusCode, result);
    }
}
