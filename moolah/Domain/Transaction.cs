using System;
using Amazon.DynamoDBv2.DataModel;

namespace moolah.Domain
{
    [DynamoDBTable("Users")]
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
    }
}