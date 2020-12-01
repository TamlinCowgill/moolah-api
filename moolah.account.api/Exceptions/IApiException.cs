using Microsoft.AspNetCore.Mvc;

namespace Moolah.Account.Api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}