using System;
using Amazon.DynamoDBv2.DataModel;

namespace moolah.common.Domain
{
    [DynamoDBTable("Moolah.Control.ItemEventProcessing")]
    public class ItemEvent
    {
        [DynamoDBHashKey] public string ItemEventId { get; set; }
        public string Body { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
