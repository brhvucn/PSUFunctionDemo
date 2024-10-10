using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSUFunctionsDemoProject.Models
{
    public class BaseModel : ITableEntity
    {
        public BaseModel() { } // Required for TableEntity
        public BaseModel(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            ETag = new ETag();
            Timestamp = DateTimeOffset.UtcNow;
        }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}


