using Microsoft.AspNetCore.Mvc;
using Moolah.Api.Domain;
using Moolah.Api.Services;

namespace Moolah.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _CustomerService;

        public CustomersController(ICustomerService service)
        {
            _CustomerService = service;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var task = _CustomerService.GetAll();
            return Ok(task.Result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer(string id)
        {
            var task = _CustomerService.GetCustomer(id);
            if (task.Result == null) return NotFound(nameof(id));

            return Ok(task.Result);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            var task = _CustomerService.CreateCustomer(customer);

            return Ok(task.Result);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult UpdateCustomer(string id, [FromBody] Customer customer)
        {
            if (customer == null) return BadRequest(nameof(customer));
            if (id != customer.Id) return BadRequest(nameof(id));

            var task = _CustomerService.UpdateCustomer(customer);

            return Ok(task.Result);
        }
    }
}