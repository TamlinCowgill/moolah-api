using Microsoft.AspNetCore.Mvc;

namespace Moolah.Api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}