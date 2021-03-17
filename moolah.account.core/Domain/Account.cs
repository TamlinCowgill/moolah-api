using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using Moolah.Account.Core.Models;

namespace Moolah.Account.Core.Domain
{
    [DynamoDBTable("Moolah.Accounts")]
    public class Account
    {
        [DynamoDBHashKey()]
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOpened { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public List<TransactionRunTotal> TransactionSummary { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Dictionary<string, string> Tags { get; set; }

    }
}

