using Microsoft.AspNetCore.Mvc;

namespace moolah.api.account.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}