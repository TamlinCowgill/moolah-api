using Microsoft.AspNetCore.Mvc;

namespace moolah.customer.api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}