using System;
using System.Collections.Generic;

namespace Moolah.Common
{
    public class MoolahException : Exception
    {
        public ErrorInfo Info { get; }

        public MoolahException(ErrorInfo info)
        {
            Info = info;
        }

        public MoolahException()
        {
        }

        public MoolahException(RpcStatusCode status, string message, Dictionary<string, string> details = null)
        {
            Info = new ErrorInfo(status, message, details);
        }
    }
}