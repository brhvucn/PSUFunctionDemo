using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp2
{
    public class ReceiveCustomerFromQueue
    {
        private readonly ILogger<ReceiveCustomerFromQueue> _logger;

        public ReceiveCustomerFromQueue(ILogger<ReceiveCustomerFromQueue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ReceiveCustomerFromQueue))]
        public void Run([QueueTrigger("customers", Connection = "")] Customer customer)
        {
            _logger.LogInformation($"Received Customer: {customer.Name}");
        }
    }
}


