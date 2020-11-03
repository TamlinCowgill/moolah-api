using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moolah.Api.Domain;
using Moolah.Api.Exceptions;
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

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpGet]
        [Route("{customerId}")]
        public IActionResult GetCustomer(string customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            return Created($"api/customers/{customer.CustomerId}", _customerService.CreateCustomer(customer));
        }

        [HttpPut, HttpPatch]
        [Route("{customerId}")]
        public IActionResult UpdateCustomer(string customerId, [FromBody] Customer customer)
        {
            if (customer == null) return BadRequest(nameof(customer));
            if (customerId != customer.CustomerId) return BadRequest(nameof(customerId));

            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpGet]
        [Route("{customerId}/accounts")]
        public IActionResult GetCustomersAccounts(string customerId)
        {
            return Ok(_accountService.GetAccountsForCustomerId(customerId));
        }

        [HttpPost]
        [Route("{customerId}/accounts")]
        public IActionResult CreateCustomerAccount(string customerId, [FromBody] Account account)
        {
            if (account == null) throw new BadRequestMissingValueException("account");
            if (account.CustomerId != customerId) throw new BadRequestInvalidValueException("customerId");

            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

            return Created($"api/customers/{customerId}/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        }
    }
}