using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Requests;
using SocialNetwork.Api.Responses.Wrappers;
using SocialNetwork.Api.Responses.Wrappers.Factories;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Data.ViewModels;

namespace SocialNetwork.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostsService _postsService;
        public PostsController(IApiResponseFactory responseFactory, IPostsService postsService)
           : base(responseFactory)
        {
            _postsService = postsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<IEnumerable<PostVM>>))]
        public async Task<IActionResult> GetPosts(int skip, int take) => ResolveServiceResponse(await _postsService.GetPosts(skip, take));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<PostVM>))]
        public async Task<IActionResult> GetPost(int id) => ResolveServiceResponse(await _postsService.GetPost(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public async Task<IActionResult> CreatePost(PostRequest request) => ResolveServiceResponse(await _postsService.CreatePost(request));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public async Task<IActionResult> DeletePost(int id) => ResolveServiceResponse(await _postsService.DeletePost(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiSuccessResponse<bool>))]
        public async Task<IActionResult> AddReaction(ReactionRequest request) => ResolveServiceResponse(await _postsService.AddReaction(request));
    }
}
