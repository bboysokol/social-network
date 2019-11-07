using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Requests;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Data.Database;
using SocialNetwork.Data.ViewModels;
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
                .Include(i => i.Comments).ThenInclude(a => a.User)
                .Include(i => i.Author)
                .Include(i => i.Reactions)
                .Where(i => !i.IsDeleted)
                .OrderByDescending(row => row)
                .Skip(skip)
                .Take(take)
                .Select(row => new PostVM()
                {
                    Id = row.Id,
                    Author = new UserVM()
                    {
                        Id = row.Author.Id,
                        Username = row.Author.UserName,
                        AvatarUrl = row.Author.AvatarUrl
                    },
                    Content = row.Content,
                    ImgUrl = row.ImgUrl,
                    CreatedAt = row.CreatedAt,
                    

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
