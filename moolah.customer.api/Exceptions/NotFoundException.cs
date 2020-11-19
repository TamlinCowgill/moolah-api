using System;
using Microsoft.AspNetCore.Mvc;

namespace moolah.customer.api.Exceptions
{
    public class NotFoundException : Exception, IApiException
    {
        public NotFoundException(string entity, string property, string identityValue) : base($"No entity of type {entity.ToLowerInvariant()} was found with {property.ToLowerInvariant()} '{identityValue}'")
        {
        }
        public IActionResult GetActionObjectResult()
        {
            return new NotFoundObjectResult(Message);
        }
    }
}