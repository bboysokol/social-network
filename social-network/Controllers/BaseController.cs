using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Responses.Wrappers.Factories;
using SocialNetwork_Backend.Services.ServiceResponses;

namespace SocialNetwork_Backend.Controllers
{

    public class BaseController : ControllerBase
    {

        protected IApiResponseFactory _responseFactory { get; set; }

        public BaseController(IApiResponseFactory responseFactory)
        {
            _responseFactory = responseFactory;
        }

        protected ObjectResult CreateErrorResponse(HttpStatusCode code, string errorMessage) => StatusCode((int)code, _responseFactory.Error(errorMessage));

        protected ObjectResult CreateSuccessResponse<T>(T data) => StatusCode((int)HttpStatusCode.OK, _responseFactory.Success(data));

        protected ObjectResult ResolveServiceResponse<T>(ServiceResponse<T> serviceResponse) => serviceResponse.Success ? CreateSuccessResponse(serviceResponse.Data) : CreateErrorResponse(HttpStatusCode.BadRequest, serviceResponse.Message);
    }
}


