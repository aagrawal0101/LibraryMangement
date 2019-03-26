using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionHandlingPoc.Common
{
    public class UserIdNotValidException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public UserIdNotValidException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
