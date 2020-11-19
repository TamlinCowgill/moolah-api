using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using AutoMapper;
using moolah.api.transaction.Domain;

namespace moolah.api.transaction.Services
{
    public class TransactionPublishService : ITransactionPublishService
    {
        private readonly IAmazonSQS _sqs;
        private readonly IAmazonSimpleNotificationService _sns;
        private readonly IMapper _mapper;

        public TransactionPublishService(IAmazonSQS sqs, IAmazonSimpleNotificationService sns, IMapper mapper)
        {
            _sqs = sqs;
            _sns = sns;
            _mapper = mapper;
        }

        public void PublishTransactionCreatedEvent(Transaction transaction)
        {
            var transactionCreatedEvent = _mapper.Map<TransactionCreatedEvent>(transaction);
            PublishTransactionCreatedSnsEvent(transactionCreatedEvent);
            //PublishTransactionCreatedSqsEvent(transactionCreatedEvent);
        }

        private void PublishTransactionCreatedSnsEvent(TransactionCreatedEvent transactionCreatedEvent)
        {
            var arn = Environment.GetEnvironmentVariable("moolah_transaction_events_arn");
            if (string.IsNullOrWhiteSpace(arn)) throw new ArgumentException(nameof(arn));

            var message = new PublishRequest
            {
                Message = JsonSerializer.Serialize(transactionCreatedEvent),
                TargetArn = arn,
                MessageAttributes = 
                {
                    ["event.type"] = new MessageAttributeValue { StringValue = "transaction.created", DataType = "String" }
                }
            };

            LambdaLogger.Log("SNS ARN: " + arn);
            LambdaLogger.Log("Sending message: " + message.Message);

            Task t = _sns.PublishAsync(message);
            t.Wait();
        }

        //private void PublishTransactionCreatedSqsEvent(TransactionCreatedEvent transactionCreatedEvent)
        //{
        //    var request = new SendMessageRequest();
        //    request.MessageAttributes["event.type"] = new MessageAttributeValue { StringValue = "transaction.created" };
        //    request.MessageAttributes["event.type"].DataType = "String";

        //    request.MessageBody = JsonSerializer.Serialize(transactionCreatedEvent);
        //    request.QueueUrl = Environment.GetEnvironmentVariable("moolah_transaction_queue_url");

        //    var message = JsonSerializer.Serialize(request);
        //    LambdaLogger.Log("SQS url: " + request.QueueUrl);
        //    LambdaLogger.Log("Sending message: " + message);

        //    _sqs.SendMessageAsync(request).Wait();
        //}
    }
}
