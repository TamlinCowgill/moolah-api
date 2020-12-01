using System;
using Microsoft.AspNetCore.Mvc;

namespace Moolah.Transaction.Api.Exceptions
{
    public class BadRequestInvalidValueException : Exception, IApiException
    {
        public BadRequestInvalidValueException(string entity) : base($"{entity.ToLowerInvariant()} contains an invalid value")
        {
        }
        public IActionResult GetActionObjectResult()
        {
            return new BadRequestObjectResult(Message);
        }

    }
}