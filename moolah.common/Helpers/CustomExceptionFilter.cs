using moolah.api.common.Exceptions;

namespace moolah.api.common.Helpers
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is IApiException)
            {
                context.Result = (context.Exception as IApiException).GetActionObjectResult();

                context.ExceptionHandled = true;
            }
        }
    }
}