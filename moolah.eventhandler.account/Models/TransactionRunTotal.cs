using System;

namespace moolah.api.account.Models
{
    public class TransactionRunTotal
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal RunningTotal { get; set; }
    }
}