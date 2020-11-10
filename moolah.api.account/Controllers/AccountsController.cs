using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using moolah.api.account.Domain;
using moolah.api.account.Exceptions;
using moolah.api.account.Helpers;
using moolah.api.account.Services;

namespace moolah.api.account.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        //private readonly ITransactionService _transactionService;
        //public AccountsController(IAccountService accountService, ITransactionService transactionService)
        //{
        //    _accountService = accountService;
        //    _transactionService = transactionService;
        //}
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
        public IActionResult CreateAccount([FromBody] Account account)
        {
            return Created($"api/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(string accountId, [FromBody] Account account)
        {
            if (account == null) throw new BadRequestMissingValueException("account");
            if (accountId != account.AccountId) throw new BadRequestInvalidValueException("accountId");

            return Ok(_accountService.UpdateAccount(account));
        }

        [HttpPatch("{accountId}")]
        public IActionResult PatchAccount(string accountId, [FromBody] JsonPatchDocument<Account> patchData)
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null) return NotFound();

            patchData.ApplyTo(account, ModelState);

            return Ok(_accountService.UpdateAccount(account));
        }

        //[HttpGet("{accountId}/transactions")]
        //public IActionResult ListTransactions(string accountId)
        //{
        //    return Ok(_transactionService.GetTransactionForAccount(accountId));
        //}
    }
}