﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moolah.Api.Common.Exceptions;

namespace Moolah.Api.Common.Helpers
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