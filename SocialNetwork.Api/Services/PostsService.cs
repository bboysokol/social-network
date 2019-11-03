using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Requests;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services
{
    public class PostsService : BaseService, IPostsService
    {

        public PostsService(SocialNetworkContext context) : base(context)
        {
        }

        public async Task<ServiceResponse<IEnumerable<PostVM>>> GetPosts(int skip, int take)
        {
            var posts = await Context.Posts
                .Where(i => !i.IsDeleted)
                .OrderByDescending(row => row)
                .Skip(skip)
                .Take(take)
                .Select(row => new PostVM()
                {
                    Author = row.UserName,
                    AuthorId = row.User.Id,
                    Content = row.Content,
                    ImgUrl = row.ImgUrl,
                    AvatarUrl = row.AvatarUrl,
                    CreateTime = row.CreateTime,
                    Id = row.Id,
                    //Comments = _context.Comments
                    //        .Where(i => i.PostId == row.Id && !i.IsDeleted)
                    //        .Select(row2 => new CommentsVM()
                    //        {
                    //            Author = $"{row2.User.UserName}",
                    //            Content = row2.Content,
                    //            CreateTime = row2.CreateTime
                    //        }).ToList(),
                    //Reactions = _context.Reactions
                    //        .Where(i => i.PostId == row.Id)
                    //        .Select(row2 => new ReactionVM()
                    //        {
                    //            Author = $"{row2.User.UserName}",
                    //        }).ToList(),
                })
                .ToListAsync();
            return ServiceResponse<IEnumerable<PostVM>>.Ok(posts);
        }
        public Task<ServiceResponse<PostVM>> GetPost(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResponse<bool>> CreatePost(PostRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeletePost(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResponse<bool>> AddReaction(ReactionRequest request)
        {
            throw new NotImplementedException();
        }    
    }
}
