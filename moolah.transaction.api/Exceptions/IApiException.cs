using Microsoft.AspNetCore.Mvc;

namespace Moolah.Transaction.Api.Exceptions
{
    public interface IApiException
    {
        IActionResult GetActionObjectResult();
    }
}