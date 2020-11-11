using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace moolah.api.account
{
    public class LambdaFunctionEntry
    {
        public async Task FunctionHandler(SQSEvent invocationEvent, ILambdaContext context)
        {
            LambdaLogger.Log("Started " + GetType().FullName + ":FunctionHandler");
            await Task.FromResult(0); // dummy awaiter

            LambdaLogger.Log(JsonSerializer.Serialize(invocationEvent));
        }
    }
}