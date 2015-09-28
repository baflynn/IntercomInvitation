using System;

namespace IntercomInvitation.Application.Providers
{
    public class JsonFileProviderException : Exception
    {
        public JsonFileProviderException(string messageFormat, params object[] messageArgs)
            : base(string.Format(messageFormat, messageArgs))
        {
        }

        public JsonFileProviderException(Exception innerException, string messageFormat, params object[] messageArgs)
            : base(string.Format(messageFormat, messageArgs), innerException)
        {
        }
    }
}