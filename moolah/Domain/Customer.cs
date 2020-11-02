using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Moolah.Api.Domain
{
    [DynamoDBTable("moolah-users")]
    public class Customer
    {
        [DynamoDBHashKey("id")] public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = "";
        public List<Account> Accounts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
