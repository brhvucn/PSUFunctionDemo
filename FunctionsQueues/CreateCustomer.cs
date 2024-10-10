using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;
using Microsoft.Extensions.Logging;
using PSUFunctionsDemoProject.Models;

namespace PSUFunctionsDemoProject.FunctionsQueues
{
    public class CreateCustomer
    {
        private readonly ILogger<CreateCustomer> _logger;

        public CreateCustomer(ILogger<CreateCustomer> logger)
        {
            _logger = logger;
        }

        [Function("CreateCustomer")]
        [QueueOutput("customers")]
        public Customer Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            Customer customer = new Customer();
            customer.Name = "John Doe";
            return customer;
        }
    }
}
