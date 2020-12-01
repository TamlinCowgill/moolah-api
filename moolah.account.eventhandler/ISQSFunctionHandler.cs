using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

namespace Moolah.Account.EventHandler
{
    public interface ISQSFunctionHandler
    {
        Task Run(SQSEvent invocationEvent, ILambdaContext context);
    }
}