using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PSUFunctionsDemoProject.Models;

namespace PSUFunctionsDemoProject.FunctionsTableStorage
{
    public class CreateCustomerFunction
    {
        private readonly ILogger<CreateCustomerFunction> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerFunction(ILogger<CreateCustomerFunction> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [Function("CreateCustomerFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("CreateCustomerFunction: Creating a new Customer");
            //convert the json POST request into a customer
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Customer customer = JsonConvert.DeserializeObject<Customer>(requestBody);
            //add the customer to the table
            await this._customerRepository.AddCustomerAsync(customer);
            return new OkObjectResult(new {result = "ok"});
        }
    }
}
