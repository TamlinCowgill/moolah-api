using Microsoft.AspNetCore.Mvc;

namespace moolah.account.api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}