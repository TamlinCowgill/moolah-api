using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Moolah.Api.Domain
{
    [DynamoDBTable("moolah-users")]
    public class Account
    {
        [DynamoDBHashKey("id")] public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime DateOpened { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public List<Transaction> Transactions;
    }
}