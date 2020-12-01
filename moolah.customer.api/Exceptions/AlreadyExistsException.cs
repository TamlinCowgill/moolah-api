using System;
using Microsoft.AspNetCore.Mvc;

namespace Moolah.Customer.Api.Exceptions
{
    public class AlreadyExistsException : Exception, IApiException
    {
        public AlreadyExistsException(string entity, string property, string identityValue) : base($"An entity of type {entity.ToLowerInvariant()} already exists with the same {property.ToLowerInvariant()} '{identityValue}'")
        {
        }

        public IActionResult GetActionObjectResult()
        {
            return new ObjectResult(Message) { StatusCode = 403 };
        }
    }
}