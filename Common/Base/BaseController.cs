using Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Common.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IAppLogger<T> _logger;
        protected IAppLogger<T> Logger
        {
            get
            {
                return _logger ?? (_logger = HttpContext.RequestServices.GetService<IAppLogger<T>>());
            }
        }
    }
}
