using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Controllers.Constants
{
    public class ApiErrorCodes
    {
        public const string BadRequest = "bad_request";
        public const string InvalidRequest = "invalid_request";
        public const string NotAuthorized = "not_authorized";
        public const string InvalidFields = "invalid_fields";
        public const string NotFound = "not_found";
        public const string InternalServiceError = "internal_service_error";
        public const string BookIdNotFound = "book_not_found";
        public const string BookIdRequestIsInvalid = "Invalid_Book_Id";
        public const string UserIdNotPresent = "User_Id_Not_Valid";
    }
}
