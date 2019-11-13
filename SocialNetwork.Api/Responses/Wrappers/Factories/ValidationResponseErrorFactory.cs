using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Responses.Wrappers.Factories
{
    public class ValidationResponseErrorFactory
    {
        /// <summary>
        /// Static factory method assigned to the InvalidModelStateResponseFactory callback
        /// Will be used by .net core to handle all ModelState Errors on endpoints with a [Route] atrribute located
        /// in controllers marked with the [ApiController] attribute
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IActionResult CreateValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                   .Where(i => i.Value.Errors.Count > 0)
                   .ToDictionary(keySelector => keySelector.Key, valueSelector => valueSelector.Value.Errors.Select(e => e.ErrorMessage).ToList());

            return new BadRequestObjectResult(new ApiValidationErrorResponse(errors));
        }
    }
}
