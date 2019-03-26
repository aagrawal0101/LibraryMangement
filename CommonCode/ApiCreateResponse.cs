using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CommonCode
{
    public class ApiCreateResponse 
    {
        public IActionResult CreateResponse(System.Net.HttpStatusCode httpStatusCode, object response)
        {
            HttpContext.Response.StatusCode = (int)httpStatusCode;
            return new ObjectResult(response);
        }
    }
}
