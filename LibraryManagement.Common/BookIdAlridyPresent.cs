using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibraryManagement.Common
{
    public class BookIdAlridyPresent : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BookIdAlridyPresent(HttpStatusCode statusCode, string message, Exception innerException)
           : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
