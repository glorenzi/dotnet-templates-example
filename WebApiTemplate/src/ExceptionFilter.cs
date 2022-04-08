using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiTemplate;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case InvalidParametersException:
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            break;
        }
    }
}
