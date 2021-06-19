using System;

namespace Support.ExceptionsManagement.Exceptions
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string message) : base(message)
        {
        }
    }
}
