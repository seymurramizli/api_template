using Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly IAppLogger<T> _logger;

        public BaseController(IAppLogger<T> logger)
        {
            _logger = logger;
        }
    }
}
