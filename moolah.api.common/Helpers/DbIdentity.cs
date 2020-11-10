using System;
using System.Text.RegularExpressions;

namespace Moolah.Api.Common.Helpers
{
    public static class DbIdentity
    {
        public static string NewId()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "").ToLowerInvariant();
        }
    }
}
