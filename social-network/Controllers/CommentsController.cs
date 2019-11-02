//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using SocialNetwork.Api.Database;
//using SocialNetwork.Api.Models;
//using SocialNetwork.Api.ViewModels;
//using SocialNetwork.Api.ViewModels.CommentVM;

//namespace SocialNetwork.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class CommentsController : BaseController
//    {
//        private readonly SocialNetworkContext _context;
//        private readonly ILogger<CommentsController> _logger = null;
//        public CommentsController(SignInManager<User> signInManager,
//            UserManager<User> userManager,
//            ILoggerFactory loggerFactory,
//            SocialNetworkContext ctx)
//           : base(signInManager, userManager, loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<CommentsController>();
//            _context = ctx;
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddComment([FromBody] AddCommentVM request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return Failure();
//            }

//            try
//            {
//                var post = _context.Posts.FirstOrDefault(i => i.Id == request.PostId && !i.IsDeleted);

//                if (post == null)
//                {
//                    return Failure();
//                }

//                //      var user = await userManager.GetUserAsync(HttpContext.User);

//                post.Comments.Add(new Comment()
//                {
//                    UserId = request.UserId,
//                    PostId = request.PostId,
//                    Content = request.Content,
//                    CreateTime = String.Format("{0:g}", DateTime.Now)

//                });

//                var result = await _context.SaveChangesAsync();

//                return Success();
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, "Error in AddComment()");
//                return Failure();
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPostComments([FromRoute]int id)
//        {
//            try
//            {
//                var comments = await _context.Comments
//                    .Where(i => i.PostId == id && !i.IsDeleted)
//                    .Select(row => new CommentsVM()
//                    {
//                        Author = $"{row.User.UserName}",
//                        Content = row.Content,
//                        CreateTime = row.CreateTime
//                    }).ToListAsync();

//                return Success(comments);
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, "Error in GetPostComments()");
//                return Failure();
//            }
//        }
//    }
//}
