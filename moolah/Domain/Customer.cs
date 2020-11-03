using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Moolah.Api.Domain
{
    [DynamoDBTable("moolah-customers")]
    public class Customer
    {
        [DynamoDBHashKey("customer-id")]
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
