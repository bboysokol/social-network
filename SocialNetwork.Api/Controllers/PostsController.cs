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
//using SocialNetwork.Api.ViewModels.ReactionVMs;

//namespace SocialNetwork.Api.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    [Authorize]
//    public class PostsController : BaseController
//    {
//        private readonly SocialNetworkContext _context;
//        private readonly ILogger<PostsController> _logger = null;

//        public PostsController(
//            SignInManager<User> signInManager,
//            UserManager<User> userManager,
//            ILoggerFactory loggerFactory,
//            SocialNetworkContext ctx)
//           : base(signInManager, userManager, loggerFactory)

//        {
//            _logger = loggerFactory.CreateLogger<PostsController>();
//            _context = ctx;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetSomePosts(int lastPost, string userId)
//        {
//            int count = 10;
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }
//            var posts = await _context.Posts
//                .Where(i => !i.IsDeleted && i.Id < filter)
//                .OrderByDescending(row => row)
//                .Take(count)
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
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                    Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                })
//                .ToListAsync();

//            return Success(posts);
//        }
//        [HttpGet]
//        public async Task<IActionResult> RefreshPosts(int lastPost, string userId, string currentPosts)
//        {
//            if (currentPosts.Contains("getsomeposts"))
//                return (await RefreshAllPosts(lastPost, userId));
//            if (currentPosts.Contains("getyourposts"))
//                return (await RefreshYourPosts(lastPost, userId));
//            if (currentPosts.Contains("getpostswhichyoulike"))
//                return (await RefreshPostsWhichYouLike(lastPost, userId));
//            return Failure();

//        }
//        public async Task<IActionResult> RefreshAllPosts(int lastPost, string userId)
//        {
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }
//            var posts = await _context.Posts
//                .Where(i => !i.IsDeleted && i.Id >= filter)
//                .OrderByDescending(row => row)
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
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                    Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                })
//                .ToListAsync();
//            return Success(posts);
//        }
//        public async Task<IActionResult> RefreshYourPosts(int lastPost, string userId)
//        {
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }
//            var posts = await _context.Posts
//                .Where(i => i.UserId == userId && !i.IsDeleted && i.Id >= filter)
//                .OrderByDescending(row => row)
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
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                    Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                }).ToListAsync();
//            return Success(posts);
//        }
//        public async Task<IActionResult> RefreshPostsWhichYouLike(int lastPost, string userId)
//        {
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }

//            var posts = await _context.Reactions
//                .Where(i => i.UserId == userId && !i.Post.IsDeleted && i.Post.Id >= filter)
//                .OrderByDescending(row => row)
//                .Select(row => new PostVM()
//                {
//                    Author = row.Post.UserName,
//                    AuthorId = row.Post.UserId,
//                    Content = row.Post.Content,
//                    ImgUrl = row.Post.ImgUrl,
//                    AvatarUrl = row.Post.AvatarUrl,
//                    CreateTime = row.Post.CreateTime,
//                    Id = row.Post.Id,
//                    Comments = _context.Comments
//                        .Where(i => i.PostId == row.Post.Id && !i.IsDeleted)
//                        .Select(row2 => new CommentsVM()
//                        {
//                            Author = $"{row2.User.UserName}",
//                            Content = row2.Content,
//                            CreateTime = row2.CreateTime
//                        }).ToList(),
//                    Reactions = _context.Reactions
//                        .Where(i => i.PostId == row.Post.Id)
//                        .Select(row2 => new ReactionVM()
//                        {
//                            Author = $"{row2.User.UserName}",
//                        }).ToList(),
//                }).ToListAsync();
//            return Success(posts);
//        }
//        // GET: api/Posts
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPosts([FromRoute]string id)
//        {

//            var posts = await _context.Posts
//                    .Where(i => !i.IsDeleted)
//                    .Select(row => new PostVM()
//                    {
//                        Author = row.UserName,
//                        AuthorId = row.User.Id,
//                        Content = row.Content,
//                        ImgUrl = row.ImgUrl,
//                        AvatarUrl = row.AvatarUrl,
//                        CreateTime = row.CreateTime,
//                        Id = row.Id,
//                        Comments = _context.Comments
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                        Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                    }).ToListAsync();

//            return Success(posts);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetYourPosts(int lastPost, string userId)
//        {
//            int count = 10;
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }
//            var posts = await _context.Posts
//                    .Where(i => i.UserId == userId && !i.IsDeleted && i.Id < filter)
//                    .OrderByDescending(row => row)
//                    .Take(count)
//                    .Select(row => new PostVM()
//                    {
//                        Author = row.UserName,
//                        AuthorId = row.User.Id,
//                        Content = row.Content,
//                        ImgUrl = row.ImgUrl,
//                        AvatarUrl = row.AvatarUrl,
//                        CreateTime = row.CreateTime,
//                        Id = row.Id,
//                        Comments = _context.Comments
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                        Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                    }).ToListAsync();

//            return Success(posts);
//        }

//        // GET: api/Posts/GetPostsWhichYouLikes/id
//        [HttpGet]
//        public async Task<IActionResult> GetPostsWhichYouLike(int lastPost, string userId)
//        {

//            int count = 10;
//            var filter = lastPost;
//            if (lastPost == 0)
//            {
//                var post = _context.Posts.Last();
//                filter = post.Id + 1;
//            }
//            var posts = await _context.Reactions
//                    .Where(i => i.UserId == userId && !i.Post.IsDeleted && i.Post.Id < filter)
//                    .OrderByDescending(row => row)
//                    .Take(count)
//                    .Select(row => new PostVM()
//                    {
//                        Author = row.Post.UserName,
//                        AuthorId = row.Post.UserId,
//                        Content = row.Post.Content,
//                        ImgUrl = row.Post.ImgUrl,
//                        AvatarUrl = row.Post.AvatarUrl,
//                        CreateTime = row.Post.CreateTime,
//                        Id = row.Post.Id,
//                        Comments = _context.Comments
//                            .Where(i => i.PostId == row.Post.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                        Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Post.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                    }).ToListAsync();
//            return Success(posts);
//        }

//        // GET: api/Posts/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPost([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var post = await _context.Posts
//                    .Where(i => i.Id == id && !i.IsDeleted)
//                    .Select(row => new PostVM()
//                    {
//                        Author = row.UserName,
//                        AuthorId = row.User.Id,
//                        Content = row.Content,
//                        ImgUrl = row.ImgUrl,
//                        AvatarUrl = row.AvatarUrl,
//                        CreateTime = row.CreateTime,
//                        Id = row.Id,
//                        Comments = _context.Comments
//                            .Where(i => i.PostId == row.Id && !i.IsDeleted)
//                            .Select(row2 => new CommentsVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                                Content = row2.Content,
//                                CreateTime = row2.CreateTime
//                            }).ToList(),
//                        Reactions = _context.Reactions
//                            .Where(i => i.PostId == row.Id)
//                            .Select(row2 => new ReactionVM()
//                            {
//                                Author = $"{row2.User.UserName}",
//                            }).ToList(),
//                    }).FirstOrDefaultAsync();


//            if (post == null)
//            {
//                return NotFound();
//            }

//            return Ok(post);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var post = new Post
//            {
//                CreateTime = postRequest.CreateTime,
//                Content = postRequest.Content,
//                UserId = postRequest.UserId,
//                UserName = postRequest.UserName,
//                AvatarUrl = postRequest.AvatarUrl,
//                ImgUrl = postRequest.ImgUrl

//            };
//            _context.Posts.Add(post);
//            await _context.SaveChangesAsync();

//            return Success();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePost([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var post = await _context.Posts.FindAsync(id);
//            if (post == null)
//            {
//                return NotFound();
//            }

//            _context.Posts.Remove(post);
//            await _context.SaveChangesAsync();

//            return Ok(post);
//        }

//        private bool PostExists(int id)
//        {
//            return _context.Posts.Any(e => e.Id == id);
//        }
//        // Post: api/Posts/5
//        [HttpPost("{id}")]
//        public async Task<IActionResult> AddReaction([FromBody] AddReactionVM request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return Failure();
//            }

//            var post = _context.Posts.FirstOrDefault(i => i.Id == request.PostId && !i.IsDeleted);

//            if (post == null)
//            {
//                return Failure();
//            }

//            var reaction = _context.Reactions.FirstOrDefault(i => i.PostId == request.PostId && i.UserId == request.UserId);

//            if(reaction == null)
//            {
//                post.Reactions.Add(new Reaction()
//                {
//                    UserId = request.UserId,
//                    PostId = request.PostId
//                });
//            }
//            else
//                _context.Reactions.Remove(reaction);

//            await _context.SaveChangesAsync();

//            return Success();


//        }
//    }
//}
