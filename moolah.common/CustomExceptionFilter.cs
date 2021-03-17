using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Moolah.Common
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is MoolahException)) return;

            var info = (context.Exception as MoolahException).Info;

            var objectResult = new ObjectResult(info)
            {
                StatusCode = (int)info.Code
            };

            context.Result = objectResult;
            context.ExceptionHandled = true;
        }
    }
}