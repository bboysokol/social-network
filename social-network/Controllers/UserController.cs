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
//using Newtonsoft.Json.Linq;
//using SocialNetwork_Backend.Database;
//using SocialNetwork_Backend.Models;
//using SocialNetwork_Backend.ViewModels.UserVMs;

//namespace SocialNetwork_Backend.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    [Authorize]
//    public class UserController : BaseController
//    {
//        private readonly SocialNetworkContext _context;
//        public UserController(
//            SignInManager<User> signInManager,
//            UserManager<User> userManager,
//            ILoggerFactory loggerFactory,
//            SocialNetworkContext ctx)
//            : base(signInManager, userManager, loggerFactory)
//        {
//            _context = ctx;
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetUsers()
//        {
//            var users = await _context.Users
//                .Where(i => !i.IsDeleted)
//                .Select(row => new UserVM()
//                {
//                    Id = row.Id,
//                    Author = row.UserName,
//                    AvatarUrl = row.AvatarUrl
//                }).ToListAsync();

//            return Success(users);
//        }

//        [HttpGet("{stringQuery}")]
//        public async Task<IActionResult> SearchUsers([FromRoute]string StringQuery)
//        {

//            if (!String.IsNullOrEmpty(StringQuery))
//            {
//                var users = await _context.Users
//                    .Where(x => EF.Functions.Like(x.UserName, $"%{StringQuery}%"))
//                    .Select(row => new UserVM()
//                    {
//                        Author = row.UserName,
//                        AvatarUrl = row.AvatarUrl,
//                        Id = row.Id
//                    }).ToListAsync();

//                return Success(users);
//            }

//            return Failure();

//        }

//        // GET: api/User/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUser([FromRoute] string id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var user = await _context.Users
//                    .Where(i => i.Id == id && !i.IsDeleted)
//                    .Select(row => new UserVM()
//                    {
//                        Author = row.UserName,
//                        AvatarUrl = row.AvatarUrl,
//                        Id = row.Id
//                    }).FirstOrDefaultAsync();

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return Ok(user);
//        }
//    }


//}
