using Microsoft.AspNetCore.Mvc;

namespace moolah.api.transaction.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}