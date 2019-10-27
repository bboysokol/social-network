//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using SocialNetwork_Backend.Database;
//using SocialNetwork_Backend.Hubs;
//using SocialNetwork_Backend.Models;
//using SocialNetwork_Backend.ViewModels.FriendVMs;

//namespace SocialNetwork_Backend.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    [Authorize]
//    public class FriendsController : BaseController
//    {
//        private readonly SocialNetworkContext _context;
//        private readonly IHubContext<NotificationHub> _friendHubContext;
//        private readonly ILogger<FriendsController> _logger = null;
//        public FriendsController(
//            SignInManager<User> signInManager,
//            UserManager<User> userManager,
//            ILoggerFactory loggerFactory,
//            SocialNetworkContext context,
//            IHubContext<NotificationHub> friendHubContext)
//            : base(signInManager, userManager, loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<FriendsController>();
//            _context = context;
//            _friendHubContext = friendHubContext;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Invite([FromBody]FriendshipVM friendRequest)
//        {
//            try
//            {

//                if (friendRequest == null)
//                {
//                    return Failure();
//                }

//                var currentUser = _context.Users.Find(friendRequest.UserId);
//                var friendUser = _context.Users.Find(friendRequest.FriendId);

//                var friend = new Friend
//                {
//                    UserId = friendRequest.UserId,
//                    FriendId = friendRequest.FriendId,
//                    FriendshipStartDate = DateTime.Now
//                };

//                _context.Friends.Add(friend);
//                await _context.SaveChangesAsync();
//                var userConnectionID = NotificationHub.GetConnectionsByUsername(currentUser.UserName);
//                var friendConnectionID = NotificationHub.GetConnectionsByUsername(friendUser.UserName);
//                await _friendHubContext.Clients.Clients(friendConnectionID).SendAsync("Added", currentUser.UserName);
//                await _friendHubContext.Clients.Clients(userConnectionID).SendAsync("Add", friendUser.UserName);
//                return Success();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in Invite()");
//                return Failure();
//            }
//        }

//        [HttpDelete]
//        public async Task<IActionResult> Delete([FromBody]FriendshipVM friendRequest)
//        {
//            try
//            {

//                if (friendRequest == null)
//                {
//                    return Failure();
//                }

//                var currentUser = _context.Users.Find(friendRequest.UserId);
//                var friendUser = _context.Users.Find(friendRequest.FriendId);

//                var friendship = _context.Friends
//                    .FirstOrDefault(i => i.UserId == currentUser.Id && !i.IsDeleted && i.FriendId == friendUser.Id);
//                if (friendship == null)
//                {
//                    return NotFound();
//                }

//                _context.Friends.Remove(friendship);


//                var userConnectionID = NotificationHub.GetConnectionsByUsername(currentUser.UserName);
//                await _friendHubContext.Clients.Clients(userConnectionID).SendAsync("Deleted", friendUser.UserName);
//                await _context.SaveChangesAsync();
//                return Success();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in Delete()");
//                return Failure();
//            }
//        }
//        // GET: api/Posts/GetFriends
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetFriends([FromRoute] string id)
//        {
//            var friends = await _context.Friends
//                    .Where(i => i.UserId == id && !i.IsDeleted && i.FriendId != id)
//                    .Select(row => new FriendVM()
//                    {
//                        UserName = row.FriendForeignKey.UserName,
//                        AvatarUrl = row.FriendForeignKey.AvatarUrl,
//                        UserId = row.FriendForeignKey.Id
//                    }).ToListAsync();

//            return Success(friends);
//        }
//        [HttpPost]
//        public bool FriendshipExist([FromBody]FriendshipVM friendRequest)
//        {
//            if (friendRequest == null)
//            {
//                return false;
//            }
//            var friendship = _context.Friends
//                    .FirstOrDefault(i => i.UserId == friendRequest.UserId && !i.IsDeleted && i.FriendId == friendRequest.FriendId);
//            if (friendship == null)
//            {
//                return false;
//            }
//            return true;

//        }
//    }
//}
