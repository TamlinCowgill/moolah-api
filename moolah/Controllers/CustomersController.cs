using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moolah.Api.Domain;
using Moolah.Api.Exceptions;
using Moolah.Api.Helpers;
using Moolah.Api.Services;

namespace Moolah.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public CustomersController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }

        [HttpGet(Name = "ListCustomerRoute")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpGet("{customerId}", Name = "GetCustomerRoute")]
        public IActionResult GetCustomer(string customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

            return Ok(customer);
        }

        [HttpPost(Name = "CreateCustomerRoute")]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            return Created($"api/customers/{customer.CustomerId}", _customerService.CreateCustomer(customer));
        }

        [HttpPut("{customerId}", Name = "UpdateCustomerRoute")]
        public IActionResult UpdateCustomer(string customerId, [FromBody] Customer customer)
        {
            if (customer == null) return BadRequest(nameof(customer));
            if (customerId != customer.CustomerId) return BadRequest(nameof(customerId));

            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpPatch("{customerId}", Name = "PatchCustomerRoute")]
        public IActionResult PatchCustomer(string customerId, [FromBody] JsonPatchDocument<Customer> patchData)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) return NotFound();

            patchData.ApplyTo(customer, ModelState);

            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpGet("{customerId}/accounts", Name = "ListCustomerAccountRoute")]
        public IActionResult ListAccounts(string customerId)
        {
            return Ok(_accountService.GetAccountsForCustomerId(customerId));
        }

        [HttpPost("{customerId}/accounts", Name = "CreateCustomerAccountRoute")]
        public IActionResult CreateCustomerAccount(string customerId, [FromBody] Account account)
        {
            if (account == null) return BadRequest();
            if (account.CustomerId != customerId) return BadRequest();

            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

            return Created($"api/customers/{customerId}/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        }
    }
}