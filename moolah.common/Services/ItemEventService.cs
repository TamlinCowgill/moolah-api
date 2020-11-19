using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using moolah.common.Domain;

namespace moolah.common.Services
{
    public class ItemEventService : IItemEventService
    {
        private IDynamoDBContext _context;

        public ItemEventService(IDynamoDBContext context)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            _context = context;
        }
        private bool EventHasBeenProcessedPreviously(string itemEventId)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            LambdaLogger.Log("itemEventId: " + itemEventId);

            return GetItemEvent(itemEventId) != null;
        }

        private ItemEvent GetItemEvent(string itemEventId)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            LambdaLogger.Log("itemEventId: " + itemEventId);
            var task = _context.LoadAsync<ItemEvent>(itemEventId);
            Task.WaitAll(task);

            return task.Result;
        }

        public ItemEvent RegisterEvent(string itemEventId, string body)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");

            if (string.IsNullOrWhiteSpace(itemEventId)) return null;
            if (EventHasBeenProcessedPreviously(itemEventId)) return null;

            var newEvent = new ItemEvent
            {
                ItemEventId = itemEventId,
                RegisteredDate = DateTime.Now,
                Body = body
            };

            LambdaLogger.Log($"Creating new event itemEventId: {itemEventId}");

            InsertItemEvent(newEvent);

            LambdaLogger.Log($"Created new event itemEventId: {itemEventId}");

            return newEvent;
        }

        public bool CommitEvent(ItemEvent itemEvent)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            if (itemEvent == null) return false;
            if (!EventHasBeenProcessedPreviously(itemEvent.ItemEventId)) return false;

            itemEvent.CompletedDate = DateTime.Now;
            _context.SaveAsync(itemEvent).Wait();

            return true;
        }

        public bool RollbackEvent(ItemEvent itemEvent)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            if (itemEvent == null) return false;
            if (!EventHasBeenProcessedPreviously(itemEvent.ItemEventId)) return false;

            _context.DeleteAsync(itemEvent.ItemEventId).Wait();

            return true;
        }

        private void InsertItemEvent(ItemEvent itemEvent)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            var table = _context.GetTargetTable<ItemEvent>();
            var document = _context.ToDocument(itemEvent);

            var expression = new Expression
            {
                ExpressionStatement = "attribute_not_exists(ItemEventId)",
                ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>()
            };

            var config = new PutItemOperationConfig
            {
                ConditionalExpression = expression
            };

            table.PutItemAsync(document, config).Wait();
        }
    }
}
