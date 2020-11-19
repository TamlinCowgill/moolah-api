using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;
using moolah.account.api.Services;
using moolah.common.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace moolah.account.api.SQS
{
    public class SQSFunctionEntry
    {
        private ServiceCollection _serviceCollection;

        public SQSFunctionEntry()
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            ConfigureServices();
        }
        private void ConfigureServices()
        {
            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            // add dependencies here
            _serviceCollection = new ServiceCollection();

            _serviceCollection
                .AddDefaultAWSOptions(new AWSOptions())
                .AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>()
                .AddSingleton<IDynamoDBContext, DynamoDBContext>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IItemEventService, ItemEventService>()
                .AddTransient<SQSFunctionHandler>();

            _serviceCollection.ToList().ForEach(s => LambdaLogger.Log(s.ToString()));
        }


        public async Task FunctionHandler(SQSEvent invocationEvent, ILambdaContext context)
        {
            await Task.FromResult(0);

            LambdaLogger.Log($"Started {GetType().FullName}::{MethodBase.GetCurrentMethod().Name}");
            LambdaLogger.Log(JsonSerializer.Serialize(invocationEvent));

            using (var serviceProvider = _serviceCollection.BuildServiceProvider())
            {
                serviceProvider.GetService<SQSFunctionHandler>().Run(invocationEvent, context);
            }
        }

    }
}