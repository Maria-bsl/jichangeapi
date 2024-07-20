using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JichangeApi.Controllers.setup
{
    public class SetupBaseController : ApiController
    {
        protected static string ERROR_OCCURED_ON_SERVER_MESSAGE = "An error occured on the server.";
        protected static string ALREADY_EXISTS_MESSAGE = "Already exists.";
        protected static string NO_DATA_FOUND_MESSAGE = "No data found.";
        protected static string NOT_FOUND_MESSAGE = "Not found.";

        protected HttpResponseMessage GetList<T,D>(T results)
        {
            if (results != null)
            {
                return GetSuccessResponse(results);
            }
            else
            {
                return GetNoDataFoundResponse();
            }
        }
        protected HttpResponseMessage GetServerErrorResponse(string errorMessage)
        {
            return Request.CreateResponse(new { response = 0, message = new List<string> { SetupBaseController.ERROR_OCCURED_ON_SERVER_MESSAGE,errorMessage } });
        }
        protected HttpResponseMessage GetAlreadyExistsErrorResponse()
        {
            return Request.CreateResponse(new { response = 0, message = new List<string> { SetupBaseController.ALREADY_EXISTS_MESSAGE } });
        }
        protected HttpResponseMessage GetInvalidModelStateResponse(List<string> errorMessages)
        {
            return Request.CreateResponse(new { response = 0, message = errorMessages });
        }
        protected HttpResponseMessage GetSuccessResponse<T>(T response)
        {
            return Request.CreateResponse(new { response = response, message = new List<string>() });
        }
        protected HttpResponseMessage GetNoDataFoundResponse()
        {
            return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { SetupBaseController.NO_DATA_FOUND_MESSAGE } });
        }
        protected HttpResponseMessage GetNotFoundResponse()
        {
            return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { SetupBaseController.NOT_FOUND_MESSAGE } });
        }
        protected HttpResponseMessage GetCustomErrorMessageResponse(List<string> messages)
        {
            return Request.CreateResponse(new { response = 0, message = messages });
        }
        protected List<string> ModelStateErrors()
        {
            if (ModelState.IsValid)
            {
                return new List<string>();
            }
            var messages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return messages;
        }
    }
}