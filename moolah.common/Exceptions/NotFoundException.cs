using System;

namespace moolah.api.common.Exceptions
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