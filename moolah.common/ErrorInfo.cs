using System.Collections.Generic;
using System.Net;

namespace Moolah.Common
{
    public class ErrorInfo
    {
        public string Status => _rpcStatusCode.ToString();
        public HttpStatusCode Code => FromRpcCode(_rpcStatusCode);
        public string Message { get; }
        public Dictionary<string, string> Details { get; }

        private readonly RpcStatusCode _rpcStatusCode;

        public ErrorInfo(RpcStatusCode status, string message = null, Dictionary<string, string> details = null)
        {
            _rpcStatusCode = status;
            Message = message;
            Details = details;
        }

        private static HttpStatusCode FromRpcCode(RpcStatusCode rpcCode)
        {
            if (rpcCode == RpcStatusCode.INVALID_ARGUMENT) return HttpStatusCode.BadRequest;
            if (rpcCode == RpcStatusCode.FAILED_PRECONDITION) return HttpStatusCode.BadRequest;
            if (rpcCode == RpcStatusCode.OUT_OF_RANGE) return HttpStatusCode.BadRequest;
            if (rpcCode == RpcStatusCode.UNAUTHENTICATED) return HttpStatusCode.Unauthorized;
            if (rpcCode == RpcStatusCode.PERMISSION_DENIED) return HttpStatusCode.Forbidden;
            if (rpcCode == RpcStatusCode.NOT_FOUND) return HttpStatusCode.NotFound;
            if (rpcCode == RpcStatusCode.ABORTED) return HttpStatusCode.Conflict;
            if (rpcCode == RpcStatusCode.ALREADY_EXISTS) return HttpStatusCode.Conflict;
            if (rpcCode == RpcStatusCode.RESOURCE_EXHAUSTED) return HttpStatusCode.TooManyRequests;
            if (rpcCode == RpcStatusCode.DATA_LOSS) return HttpStatusCode.InternalServerError;
            if (rpcCode == RpcStatusCode.UNKNOWN) return HttpStatusCode.InternalServerError;
            if (rpcCode == RpcStatusCode.INTERNAL) return HttpStatusCode.InternalServerError;
            if (rpcCode == RpcStatusCode.NOT_IMPLEMENTED) return HttpStatusCode.NotImplemented;
            if (rpcCode == RpcStatusCode.UNAVAILABLE) return HttpStatusCode.ServiceUnavailable;
            if (rpcCode == RpcStatusCode.DEADLINE_EXCEEDED) return HttpStatusCode.GatewayTimeout;
            
            return HttpStatusCode.BadRequest;
        }
    }
}