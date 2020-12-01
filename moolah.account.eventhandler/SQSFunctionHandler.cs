using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Moolah.Account.Core.Models;
using Moolah.Account.Core.Services;
using moolah.common.Domain;
using moolah.common.Services;

namespace Moolah.Account.EventHandler
{
    public class SQSFunctionHandler 
    {
        private readonly IItemEventService _itemEventService;
        private readonly IAccountService _accountService;

        public SQSFunctionHandler(IItemEventService itemEventService, IAccountService accountService)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            _itemEventService = itemEventService;
            _accountService = accountService;
        }

        public void Run(SQSEvent invocationEvent, ILambdaContext context)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
         
            var records = invocationEvent.Records
                .Where(r => r.MessageAttributes.ContainsKey("event.type"))
                .Where(r => r.MessageAttributes["event.type"].StringValue == "transaction.created")
                .ToList();

            LambdaLogger.Log(records.Count() + " transaction.created records found");
            if (!records.Any()) return;

            ProcessNewTransactions(records);
        }


        private void ProcessNewTransactions(List<SQSEvent.SQSMessage> records)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            //Parallel.ForEach(records, ProcessNewTransaction);
            records.ForEach(ProcessNewTransaction);
        }

        private void ProcessNewTransaction(SQSEvent.SQSMessage message)
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            LambdaLogger.Log("Processing message " + message.MessageId);
            ItemEvent itemEvent = null;
            try
            {
                // Evaluate that the message body meets the transaction structure
                var transaction = JsonSerializer.Deserialize<Transaction>(message.Body);
                if (transaction == null)
                {
                    LambdaLogger.Log("Unable to deserialize message body");
                    return;
                }
                LambdaLogger.Log("Deserialized message body");

                // Ensure we have not processed this message previously
                itemEvent = _itemEventService.RegisterEvent(message.MessageId, message.Body);
                if (itemEvent == null)
                {
                    LambdaLogger.Log("Unable to register event for message " + message.MessageId);
                    return;
                }

                LambdaLogger.Log("Appending transaction " + transaction.TransactionId);
                _accountService.AppendTransaction(transaction);

                // Set a time that 
                LambdaLogger.Log("Committing event " + message.MessageId);
                _itemEventService.CommitEvent(itemEvent);
            }
            catch
            {
                LambdaLogger.Log("Rolling back event " + message.MessageId);
                _itemEventService.RollbackEvent(itemEvent);
                throw;
            }
        }
    }
}
