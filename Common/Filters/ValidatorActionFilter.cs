using Common.Contract;
using Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var response = new ApiResponse
                {
                    Code = ResponseCode.ValidationError,
                    Message = string.Join(", ", filterContext.ModelState.Values.SelectMany(v => v.Errors).Select(a => a.ErrorMessage))
                };

                filterContext.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
