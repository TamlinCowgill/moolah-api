using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moolah.Customer.Api.Exceptions;
using Moolah.Customer.Api.Helpers;
using Moolah.Customer.Core.Services;

namespace Moolah.Customer.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomer(string customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Core.Domain.Customer customer)
        {
            return Created($"api/customers/{customer.CustomerId}", _customerService.CreateCustomer(customer));
        }

        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomer(string customerId, [FromBody] Core.Domain.Customer customer)
        {
            if (customer == null) return BadRequest(nameof(customer));
            if (customerId != customer.CustomerId) return BadRequest(nameof(customerId));

            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpPatch("{customerId}")]
        public IActionResult PatchCustomer(string customerId, [FromBody] JsonPatchDocument<Core.Domain.Customer> patchData)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer == null) return NotFound();

            patchData.ApplyTo(customer, ModelState);

            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpGet("{customerId}/accounts")]
        public IActionResult ListAccounts(string customerId)
        {
            //return Ok(_accountService.GetAccountsForCustomerId(customerId));
            return null;
        }

        //[HttpPost("{customerId}/accounts")]
        //public IActionResult CreateCustomerAccount(string customerId, [FromBody] Account account)
        //{
        //    if (account == null) return BadRequest();
        //    if (account.CustomerId != customerId) return BadRequest();

        //    var customer = _customerService.GetCustomer(customerId);
        //    if (customer == null) throw new NotFoundException("customer", "customerid", customerId);

        //    return Created($"api/customers/{customerId}/accounts/{account.AccountId}", _accountService.CreateAccount(account));
        //}
    }
}