using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace moolah.transaction.api.Domain
{
    [DynamoDBTable("Moolah.Transactions")]
    public class Transaction
    {
        [DynamoDBHashKey()]
        public string TransactionId { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    }
}