using System;

namespace Support.ExceptionsManagement.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string mensagem) : base(mensagem)
        {
        }
    }
}
