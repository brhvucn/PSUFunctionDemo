using PSUFunctionsDemoProject.Models;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PSUFunctionsDemoProject.FunctionsTableStorage
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TableClient tableClient;
        public CustomerRepository(IConfiguration config)
        {
            string connectionString = config["AzureWebJobsStorage"].ToString();
            var serviceClient = new TableServiceClient(connectionString);
            //Create a new table client
            tableClient = serviceClient.GetTableClient(Names.Tables.CustomerTable);
            //Create the table if it does not already exist
            tableClient.CreateIfNotExists();
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            await this.tableClient.AddEntityAsync(customer);
        }

        public async Task<Customer> GetCustomerAsync(string partitionKey, string rowKey)
        {
            return await this.tableClient.GetEntityAsync<Customer>(partitionKey, rowKey);
        }

        public async Task<List<Customer>> GetCustomerWithFilterAsync(string filter)
        {
            var customers = new List<Customer>();
            await foreach (var customerPage in this.tableClient.QueryAsync<Customer>(filter).AsPages())
            {
                customers.AddRange(customerPage.Values);
            }
            return customers;
        }
    }
}



