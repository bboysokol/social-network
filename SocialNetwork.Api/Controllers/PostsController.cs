using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //   
        //    [HttpGet("{id}")]
        //    public async Task<IActionResult> GetPost([FromRoute] int id)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var post = await _context.Posts
        //                .Where(i => i.Id == id && !i.IsDeleted)
        //                .Select(row => new PostVM()
        //                {
        //                    Author = row.UserName,
        //                    AuthorId = row.User.Id,
        //                    Content = row.Content,
        //                    ImgUrl = row.ImgUrl,
        //                    AvatarUrl = row.AvatarUrl,
        //                    CreateTime = row.CreateTime,
        //                    Id = row.Id,
        //                    Comments = _context.Comments
        //                        .Where(i => i.PostId == row.Id && !i.IsDeleted)
        //                        .Select(row2 => new CommentsVM()
        //                        {
        //                            Author = $"{row2.User.UserName}",
        //                            Content = row2.Content,
        //                            CreateTime = row2.CreateTime
        //                        }).ToList(),
        //                    Reactions = _context.Reactions
        //                        .Where(i => i.PostId == row.Id)
        //                        .Select(row2 => new ReactionVM()
        //                        {
        //                            Author = $"{row2.User.UserName}",
        //                        }).ToList(),
        //                }).FirstOrDefaultAsync();


        //        if (post == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(post);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var post = new Post
        //        {
        //            CreateTime = postRequest.CreateTime,
        //            Content = postRequest.Content,
        //            UserId = postRequest.UserId,
        //            UserName = postRequest.UserName,
        //            AvatarUrl = postRequest.AvatarUrl,
        //            ImgUrl = postRequest.ImgUrl

        //        };
        //        _context.Posts.Add(post);
        //        await _context.SaveChangesAsync();

        //        return Success();
        //    }

        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeletePost([FromRoute] int id)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var post = await _context.Posts.FindAsync(id);
        //        if (post == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Posts.Remove(post);
        //        await _context.SaveChangesAsync();

        //        return Ok(post);
        //    }

        //    private bool PostExists(int id)
        //    {
        //        return _context.Posts.Any(e => e.Id == id);
        //    }
        //    // Post: api/Posts/5
        //    [HttpPost("{id}")]
        //    public async Task<IActionResult> AddReaction([FromBody] ReactionRequest request)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Failure();
        //        }

        //        var post = _context.Posts.FirstOrDefault(i => i.Id == request.PostId && !i.IsDeleted);

        //        if (post == null)
        //        {
        //            return Failure();
        //        }

        //        var reaction = _context.Reactions.FirstOrDefault(i => i.PostId == request.PostId && i.UserId == request.UserId);

        //        if (reaction == null)
        //        {
        //            post.Reactions.Add(new Reaction()
        //            {
        //                UserId = request.UserId,
        //                PostId = request.PostId
        //            });
        //        }
        //        else
        //            _context.Reactions.Remove(reaction);

        //        await _context.SaveChangesAsync();

        //        return Success();


        //    }
    }
}
