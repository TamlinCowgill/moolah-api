using Microsoft.AspNetCore.Mvc;

namespace Moolah.Customer.Api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}