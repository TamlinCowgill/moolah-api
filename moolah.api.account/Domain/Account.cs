using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace moolah.api.account.Domain
{
    [DynamoDBTable("Moolah.Accounts")]
    public class Account
    {
        [DynamoDBHashKey()]
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOpened { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Dictionary<string, string> Tags { get; set; }

    }
}

