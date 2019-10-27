using System.Collections.Generic;

namespace SocialNetwork_Backend.Responses.Wrappers
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public Dictionary<string,List<string>> Errors { get; set; }

        public ApiValidationErrorResponse(Dictionary<string,List<string>> errorDictionary) : base(false)
        {
            Errors = errorDictionary;
        }
    }
}
