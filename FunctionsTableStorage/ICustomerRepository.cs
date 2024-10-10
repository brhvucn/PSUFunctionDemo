using PSUFunctionsDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSUFunctionsDemoProject.FunctionsTableStorage
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(string partitionKey, string rowKey);
        Task<List<Customer>> GetCustomerWithFilterAsync(string filter);
    }
}
