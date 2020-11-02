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
            var task = _accountService.GetAll();
            return Ok(task.Result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAccount(string id)
        {
            var task = _accountService.GetAccount(id);
            if (task.Result == null) return NotFound(nameof(id));

            return Ok(task.Result);
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] Account account)
        {
            var task = _accountService.CreateAccount(account);

            return Ok(task.Result);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult UpdateAccount(string id, [FromBody] Account account)
        {
            if (account == null) return BadRequest(nameof(account));
            if (id != account.Id) return BadRequest(nameof(id));

            var task = _accountService.UpdateAccount(account);

            return Ok(task.Result);
        }
    }
}