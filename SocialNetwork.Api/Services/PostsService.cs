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
                .Where(i => !i.IsDeleted)
                .OrderByDescending(row => row)
                .Skip(skip)
                .Take(take)
                .Include(i => i.Comments).ThenInclude(a => a.Author)
                .Include(i => i.Author)
                .Include(i => i.Reactions)
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
                    Comments = row.Comments.Select(row => new CommentVM()
                    {
                        Author =  new UserVM()
                        {
                            Id = row.Author.Id,
                            Username = row.Author.UserName,
                            AvatarUrl = row.Author.AvatarUrl
                        },
                        Content = row.Content,
                        CreatedAt = row.CreatedAt

                    }).ToList(),
                    Reactions = row.Reactions.Select(row => new ReactionVM()
                    {
                        User = new UserVM()
                        {
                            Id = row.Author.Id,
                            Username = row.Author.UserName,
                            AvatarUrl = row.Author.AvatarUrl
                        }
                    }).ToList()
                })
                .ToListAsync();
            return ServiceResponse<IEnumerable<PostVM>>.Ok(posts);
        }
        public async Task<ServiceResponse<PostVM>> GetPost(int id)
        {
            var post = await Context.Posts
                .Include(i => i.Comments).ThenInclude(a => a.Author)
                .Include(i => i.Author)
                .Include(i => i.Reactions)
                .FirstOrDefaultAsync(i => !i.IsDeleted && i.Id == id);

            var postDto =  new PostVM()
            {
                Id = post.Id,
                Author = new UserVM()
                {
                    Id = post.Author.Id,
                    Username = post.Author.UserName,
                    AvatarUrl = post.Author.AvatarUrl
                },
                Content = post.Content,
                ImgUrl = post.ImgUrl,
                CreatedAt = post.CreatedAt,
                Comments = post.Comments.Select(row => new CommentVM()
                {
                    Author = new UserVM()
                    {
                        Id = row.Author.Id,
                        Username = row.Author.UserName,
                        AvatarUrl = row.Author.AvatarUrl
                    },
                    Content = row.Content,
                    CreatedAt = row.CreatedAt

                }).ToList(),
                Reactions = post.Reactions.Select(row => new ReactionVM()
                {
                    User = new UserVM()
                    {
                        Id = row.Author.Id,
                        Username = row.Author.UserName,
                        AvatarUrl = row.Author.AvatarUrl
                    }
                }).ToList()
            };

            return ServiceResponse<PostVM>.Ok(postDto);
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
