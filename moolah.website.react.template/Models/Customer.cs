using System;
using System.Collections.Generic;

namespace moolah.website.react.template.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public double Age => Math.Floor(DateTime.Today.Subtract(DateOfBirth).TotalDays / 365);
    }
}
