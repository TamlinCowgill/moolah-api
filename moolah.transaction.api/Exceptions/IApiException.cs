using Microsoft.AspNetCore.Mvc;

namespace moolah.transaction.api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}