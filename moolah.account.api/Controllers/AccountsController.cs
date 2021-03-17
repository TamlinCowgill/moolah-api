using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moolah.Account.Core.Services;
using Moolah.Common;

namespace Moolah.Account.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("[controller]")]
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
            if (account == null) throw new MoolahException(RpcStatusCode.NOT_FOUND, nameof(accountId) + ":" + accountId);

            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Moolah.Account.Core.Domain.Account account)
        {
            var newAccount = _accountService.CreateAccount(account);
            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{account.AccountId}", newAccount);
        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(string accountId, [FromBody] Moolah.Account.Core.Domain.Account account)
        {
            if (!TryValidateModel(account)) throw new MoolahException(RpcStatusCode.INVALID_ARGUMENT, nameof(account));
            if (accountId != account.AccountId) throw new MoolahException(RpcStatusCode.INVALID_ARGUMENT, "resource and body parameter mismatch:" + nameof(account));

            return Ok(_accountService.UpdateAccount(account));
        }

        [HttpPatch("{accountId}")]
        public IActionResult PatchAccount(string accountId, [FromBody] JsonPatchDocument<Moolah.Account.Core.Domain.Account> patchData)
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null) throw new MoolahException(RpcStatusCode.NOT_FOUND, nameof(accountId) + ":" + accountId);

            patchData.ApplyTo(account, ModelState);

            return Ok(_accountService.UpdateAccount(account));
        }
    }
}