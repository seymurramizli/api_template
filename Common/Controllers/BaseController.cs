using Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Common.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {

    }
}
