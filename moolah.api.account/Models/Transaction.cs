using System;
using System.Collections.Generic;

namespace moolah.api.account.Models
{
    public class Transaction
    {
        public EventMetaData Meta => new EventMetaData { EventType = "transaction.create" };
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