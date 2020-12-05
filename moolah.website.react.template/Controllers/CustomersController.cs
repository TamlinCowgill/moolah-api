using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using moolah.website.react.template.Configuration;
using moolah.website.react.template.Models;

namespace moolah.website.react.template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ApiResources _apiResources;

        public CustomersController(IOptions<ApiResources> apiResourcesOptions, ILogger<CustomersController> logger)
        {
            _apiResources = apiResourcesOptions.Value;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var task = GetAsync(_apiResources.CustomersUri);
            task.Wait();

            return task.Result;
        }


        private async Task<Customer[]> GetAsync(string uri)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(_apiResources.CustomersUri);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Customer[]>(content, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true });
        }
    }
}
