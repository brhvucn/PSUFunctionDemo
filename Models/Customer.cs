using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSUFunctionsDemoProject.Models
{
    public class Customer : BaseModel
    {        
        public Customer() : base(Guid.NewGuid().ToString(), 
            Guid.NewGuid().ToString())
        {
            Name = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}


