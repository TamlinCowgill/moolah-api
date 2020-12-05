using System;
using System.Collections.Generic;

namespace moolah.website.react.template.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOpened { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionRunTotal> TransactionSummary { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Dictionary<string, string> Tags { get; set; }

    }
}

