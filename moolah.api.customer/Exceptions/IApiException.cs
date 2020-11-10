using Microsoft.AspNetCore.Mvc;

namespace moolah.api.customer.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}