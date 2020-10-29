using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace moolah.Domain
{
    [DynamoDBTable("Users")]
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOpened { get; set; }
        public decimal Balance { get; set; }

        public List<Transaction> Transactions;
    }
}