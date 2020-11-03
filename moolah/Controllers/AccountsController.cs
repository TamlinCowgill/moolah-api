using Microsoft.AspNetCore.Mvc;
using Moolah.Api.Domain;
using Moolah.Api.Services;

namespace Moolah.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService service)
        {
            _accountService = service;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accountService.GetAll());
        }

        [HttpGet]
        [Route("{accountId}")]
        public IActionResult GetAccount(string accountId)
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null) return NotFound(nameof(accountId));

            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            return Created($"api/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        }

        [HttpPut, HttpPatch]
        [Route("{accountId}")]
        public IActionResult UpdateAccount(string accountId, [FromBody] Account account)
        {
            if (account == null) return BadRequest(nameof(account));
            if (string.IsNullOrWhiteSpace(accountId)) return BadRequest(nameof(accountId));
            if (accountId != account.AccountId) return BadRequest(nameof(accountId));

            return Ok(_accountService.UpdateAccount(account));
        }
    }
}