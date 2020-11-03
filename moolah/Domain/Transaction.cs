using System;
using Amazon.DynamoDBv2.DataModel;

namespace Moolah.Api.Domain
{
    [DynamoDBTable("moolah-transactions")]
    public class Transaction
    {
        [DynamoDBHashKey("transaction-id")]
        public string TransactionId { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}