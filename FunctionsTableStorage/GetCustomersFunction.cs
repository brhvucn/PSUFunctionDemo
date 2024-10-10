using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PSUFunctionsDemoProject.FunctionsTableStorage
{
    public class GetCustomersFunction
    {
        private readonly ILogger<GetCustomersFunction> _logger;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersFunction(ILogger<GetCustomersFunction> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [Function("GetCustomersFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("Get a list of customers using the filter");
            var customers = await this._customerRepository.GetCustomerWithFilterAsync($"Name eq 'Peter Parker'");
            return new OkObjectResult(customers);
        }
    }
}
