using ExceptionHandlingPoc.Common;
using LibraryManagement.BusinessLayers;
using LibraryManagement.Common;
using LibraryManagement.Controllers.Constants;
using LibraryManagement.Mappers;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using LibraryModel.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class LibraryController : BaseController
    {
        ILibraryManager _libraryManager;
        public LibraryController(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("library/newBook")]
        public async Task<IActionResult> AddNewBook([FromBody]AddNewBookRequest newBookRequest)
        {
            var msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));

            if (!ModelState.IsValid)
            {
                msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ModelState));
            }
            else
            {
                try
                {
                    BookDomain newBookDomain = await _libraryManager.AddNewBook(newBookRequest).ConfigureAwait(false);

                    if (newBookDomain == null)
                    {
                        msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.BadRequest));
                    }
                    else
                    {
                        msg = CreateResponse(HttpStatusCode.Created, newBookDomain);
                    }
                }

                catch (BookIdAlridyPresent ex)
                {
                    msg = CreateErrorResponse(ex.StatusCode, ApiErrorCodes.BookIdNotFound);
                }
                catch (Exception ex)
                {
                    msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));
                }
            }
            return msg;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("library/getAllBook")]
        public async Task<IActionResult> Get()
        {
            var msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));

            if (!ModelState.IsValid)
            {
                msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ModelState));
            }
            else
            {
                try
                {
                    IList<BookDomain> BookList = await _libraryManager.GetList().ConfigureAwait(false);

                    if (BookList != null)
                    {
                        msg = CreateResponse(HttpStatusCode.OK, BookList);
                    }

                    else
                    {
                        msg = CreateResponse(HttpStatusCode.NotFound, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.NotFound));
                    }
                }
                catch (Exception ex)
                {
                    msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));
                }
            }
            return msg;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("library/getBookById/{BookId}")]
        public async Task<IActionResult> GetBookById(int BookId)
        {
            var msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));

            if (!ModelState.IsValid)
            {
                msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ModelState));
            }
            else
            {
                try
                {
                    if (BookId <= 0)
                    {
                        return CreateResponse(HttpStatusCode.NotFound,
                            ApiErrorProvider.GetErrorResponse(ApiErrorCodes.NotFound));
                    }

                    BookDomain bookInfo = await _libraryManager.GetBookInfoByBookId(BookId).ConfigureAwait(false);

                    if (bookInfo == null)
                    {
                        return CreateResponse(HttpStatusCode.NotFound,
                            ApiErrorProvider.GetErrorResponse(ApiErrorCodes.BookIdRequestIsInvalid));
                    }

                    msg = CreateResponse(HttpStatusCode.OK, bookInfo);
                }
                catch (Exception ex)
                {
                    msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));
                }
            }

            return msg;
        }

        [HttpDelete]
        [Produces("application/json")]
        [Route("library/deleteBookById/{BookId}")]
        public async Task<IActionResult> DeleteBookById(int BookId)
        {
            var msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));

            try
            {
                if (!ModelState.IsValid)
                {
                    msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ModelState));
                }

                if (BookId <= 0)
                {
                    return CreateResponse(HttpStatusCode.NotFound,
                        ApiErrorProvider.GetErrorResponse(ApiErrorCodes.NotFound));
                }

                BookDomain bookInfo = await _libraryManager.DeleteBookByBookId(BookId).ConfigureAwait(false);

                if (bookInfo == null)
                {
                    return CreateResponse(HttpStatusCode.NotFound,
                        ApiErrorProvider.GetErrorResponse(ApiErrorCodes.BookIdRequestIsInvalid));
                }

                msg = CreateResponse(HttpStatusCode.OK, bookInfo);
            }

            catch (UserIdNotValidException ex)
            {
                msg = CreateResponse(HttpStatusCode.NotFound, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.UserIdNotPresent));
            }
            catch (Exception ex)
            {
                msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));
            }

            return msg;
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("library/UpdateBookById/{BookId}")]
        public async Task<IActionResult> UpdateBookById([FromBody] UpdateNewBookRequest updateNewBook)
        {
            var msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));

            if (!ModelState.IsValid)
            {
                msg = CreateResponse(HttpStatusCode.BadRequest, ApiErrorProvider.GetErrorResponse(ModelState));
            }

            try
            {
                BookDomain bookInfo = await _libraryManager.UpdateBookByBookId(updateNewBook).ConfigureAwait(false);

                if (bookInfo == null)
                {
                    return CreateResponse(HttpStatusCode.NotFound,
                        ApiErrorProvider.GetErrorResponse(ApiErrorCodes.BookIdRequestIsInvalid));
                }

                msg = CreateResponse(HttpStatusCode.OK, bookInfo);
            }

            catch (UserIdNotValidException ex)
            {
                msg = CreateResponse(HttpStatusCode.NotFound, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.UserIdNotPresent));
            }
            catch (Exception ex)
            {
                msg = CreateResponse(HttpStatusCode.InternalServerError, ApiErrorProvider.GetErrorResponse(ApiErrorCodes.InternalServiceError));
            }

            return msg;
        }
    }
}


