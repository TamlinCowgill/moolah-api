using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

namespace moolah.api.account.SQS
{
    public interface ISQSFunctionHandler
    {
        Task Run(SQSEvent invocationEvent, ILambdaContext context);
    }
}