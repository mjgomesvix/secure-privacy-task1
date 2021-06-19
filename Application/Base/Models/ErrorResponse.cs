using System.Collections.Generic;

namespace ApplicationLayer.Base.Models
{
    public class ErrorResponse
    {
        private readonly List<string> _erros;

        public ErrorResponse(int statusCode)
        {
            _erros = new List<string>();
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
        public IEnumerable<string> Errors => _erros;

        public void Add(string error)
        {
            _erros.Add(error);
        }

        public void Add(IEnumerable<string> errors)
        {
            _erros.AddRange(errors);
        }
    }
}
