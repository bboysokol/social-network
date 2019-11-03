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
//using SocialNetwork.Api.Database;
//using SocialNetwork.Api.Models;
//using SocialNetwork.Api.Requests.NotificationVMs;

//namespace SocialNetwork.Api.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    [Authorize]
//    public class NotificationsController : BaseController
//    {
//        private readonly SocialNetworkContext _context;
//        private readonly ILogger<NotificationsController> _logger = null;
//        public NotificationsController(SignInManager<User> signInManager,
//            UserManager<User> userManager,
//            ILoggerFactory loggerFactory,
//            SocialNetworkContext ctx)
//           : base(signInManager, userManager, loggerFactory)
//        {
//            _context = ctx;
//        }

//        // GET: api/Notifications
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetNotifications([FromRoute]string id)
//        {
//            try
//            {
//                var notifications = await _context.Notifications
//                    .Where(i => i.User.Id == id && !i.IsDeleted)
//                    .Select(row => new NotificationVM()
//                    {
//                        Id = row.Id,
//                        Content = row.Content,
//                        CreateTime = row.CreateTime,
//                        IsReaded = row.IsReaded

//                    }).ToListAsync();

//                return Success(notifications);
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, "Error in GetPostComments()");
//                return Failure();
//            }
//        }

//        // POST: api/Notifications
//        [HttpPost]
//        public async Task<IActionResult> CreateNotification([FromBody]JObject body)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var notification = new Notification
//            {
//                Content = body.GetValue("Content").Value<string>(),
//                UserId = body.GetValue("UserId").Value<string>(),
//                CreateTime = DateTime.Now.ToString(),
//                IsDeleted = false,
//                IsReaded = false
//            };

//            _context.Notifications.Add(notification);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
//        }

//        // DELETE: api/Notifications/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var notification = await _context.Notifications.FindAsync(id);
//            if (notification == null)
//            {
//                return NotFound();
//            }

//            _context.Notifications.Remove(notification);
//            await _context.SaveChangesAsync();

//            return Ok(notification);
//        }

//        [HttpPost("{id}")]
//        public async Task<IActionResult> ReadNotification([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var notification = await _context.Notifications.FindAsync(id);
//            if (notification == null)
//            {
//                return NotFound();
//            }
//            notification.IsReaded = true;
//            await _context.SaveChangesAsync();

//            return Success();
//        }
//    }
//}
