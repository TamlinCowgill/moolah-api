using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Moolah.Customer.Core.Domain
{
    [DynamoDBTable("Moolah.Customers")]
    public class Customer
    {
        [DynamoDBHashKey()]
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}
