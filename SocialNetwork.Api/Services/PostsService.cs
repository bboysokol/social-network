using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Requests;
using SocialNetwork.Api.Services.Interfaces;
using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Data.Database;
using SocialNetwork.Data.Models;
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
            try
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
                            Author = new UserVM()
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
            catch(Exception ex)
            {
                return ServiceResponse<IEnumerable<PostVM>>.Error("You have no posts to display");
            }
            
        }
        public async Task<ServiceResponse<PostVM>> GetPost(int id)
        {
            try
            {
                var post = await Context.Posts
                    .Include(i => i.Comments).ThenInclude(a => a.Author)
                    .Include(i => i.Author)
                    .Include(i => i.Reactions)
                    .FirstOrDefaultAsync(i => !i.IsDeleted && i.Id == id);

                var postDto = new PostVM()
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
            catch(Exception ex)
            {
                return ServiceResponse<PostVM>.Error("This post doesn't exist");
            }
        }
        public async Task<ServiceResponse<bool>> CreatePost(PostRequest request)
        {
            try
            {
                var post = new Post()
                {
                    ImgUrl = request.ImgUrl,
                    Content = request.Content,
                    AuthorId = request.AuthorId
                };

                Context.Posts.Add(post);
                await Context.SaveChangesAsync();

                return ServiceResponse<bool>.Ok();
            }catch(Exception ex)
            {
                return ServiceResponse<bool>.Error("There was a problem adding the post. Please try later");
            }
        }

        public async Task<ServiceResponse<bool>> DeletePost(int id)
        {
            var post = await Context.Posts.FirstOrDefaultAsync(i => i.Id == id);
            if(post == null)
                return ServiceResponse<bool>.Error("This post doesn't exist");

            Context.Posts.Remove(post);
            await Context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok();
        }
        public async Task<ServiceResponse<bool>> AddReaction(ReactionRequest request)
        {
            try
            {
                var post = await Context.Posts.FirstOrDefaultAsync(i => i.Id == request.PostId);
                if(post == null)
                    return ServiceResponse<bool>.Error("This post doesn't exist");

                var reaction = new Reaction()
                {
                    AuthorId = request.AuthorId,
                    PostId = request.PostId
                };

                Context.Reactions.Add(reaction);
                await Context.SaveChangesAsync();

                return ServiceResponse<bool>.Ok();
            }catch(Exception ex)
            {
                return ServiceResponse<bool>.Error("You can't add reaction to this post");
            }
        }
    }
}
