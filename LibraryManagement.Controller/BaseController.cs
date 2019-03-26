using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Test.LibraryManagement.CommonError;

namespace LibraryManagement.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApiErrorProvider ApiErrorProvider { get; }
        protected ILogger _logger { get; set; }

        public BaseController()
        {
           ApiErrorProvider = new ApiErrorProvider();
        }

        public static ErrorServiceResponse GetErrorResponse(string code, string description)
        {
            return new ErrorServiceResponse
            {
                Errors = new List<ErrorResponse> { new ErrorResponse(code, description, null) }
            };
        }
        public IActionResult CreateErrorResponse(HttpStatusCode httpStatusCode, string apiErrorCodes, Exception exception = null)
        {
            HttpContext.Response.StatusCode = (int)httpStatusCode;

            return new ObjectResult(new { apiErrorCodes, message  = "", exception = exception });
        }
        public IActionResult CreateResponse(HttpStatusCode httpStatusCode, object response)
        {
            HttpContext.Response.StatusCode = (int)httpStatusCode;
            return new ObjectResult(response);
        }

        protected IActionResult UnauthorizedAccessResponse()
        {
            var code = "Service.NotAuthorized";
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return ErrorResponse(code, string.Empty);
        }

        protected IActionResult ObjectNotFoundResponse(Type type)
        {
            var code = "NotFound";
            var message = string.Format("Object Not Found: '{0}'.", type != null ? type.Name : "[null]");
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            return ErrorResponse(code, message);
        }

        protected IActionResult MissingParameterResponse(string paramName)
        {
            var code = "BadRequest.MissingParameter";
            var message = string.Format("Missing Parameter: '{0}'.", paramName ?? "[null]");
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return ErrorResponse(code, message);
        }

        protected IActionResult NoContentResponse()
        {
            return new NoContentResult();
        }

        protected IActionResult InternalServerResponse()
        {
            var code = HttpStatusCode.InternalServerError.ToString();
            HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return ErrorResponse(code, string.Empty);
        }


        private IActionResult ErrorResponse(string code, string message)
        {
            return new ObjectResult(new { code, message });
        }
    }
}
