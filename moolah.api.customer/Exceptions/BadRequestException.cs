using System;
using Microsoft.AspNetCore.Mvc;

namespace moolah.api.customer.Exceptions
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