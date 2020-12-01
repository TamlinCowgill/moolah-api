using System;
using Microsoft.AspNetCore.Mvc;

namespace Moolah.Customer.Api.Exceptions
{
    public class BadRequestMissingValueException : Exception, IApiException
    {
        public BadRequestMissingValueException(string entity) : base($"{entity.ToLowerInvariant()} is missing")
        {
        }
        public IActionResult GetActionObjectResult()
        {
            return new BadRequestObjectResult(Message);
        }

    }
}