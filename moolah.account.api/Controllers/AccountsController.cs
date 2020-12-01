using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moolah.Account.Api.Exceptions;
using Moolah.Account.Api.Helpers;
using Moolah.Account.Core.Services;

namespace Moolah.Account.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accountService.GetAll());
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccount(string accountId)
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null) return NotFound(nameof(accountId));

            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Moolah.Account.Core.Domain.Account account)
        {
            return Created($"api/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(string accountId, [FromBody] Moolah.Account.Core.Domain.Account account)
        {
            if (account == null) throw new BadRequestMissingValueException("account");
            if (accountId != account.AccountId) throw new BadRequestInvalidValueException("accountId");

            return Ok(_accountService.UpdateAccount(account));
        }

        [HttpPatch("{accountId}")]
        public IActionResult PatchAccount(string accountId, [FromBody] JsonPatchDocument<Moolah.Account.Core.Domain.Account> patchData)
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null) return NotFound();

            patchData.ApplyTo(account, ModelState);

            return Ok(_accountService.UpdateAccount(account));
        }
    }
}