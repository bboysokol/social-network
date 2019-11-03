using SocialNetwork.Api.Services.ServiceResponses;
using SocialNetwork.Api.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services.Interfaces
{
    public interface IPostsService
    {
        public Task<ServiceResponse<IEnumerable<PostVM>>> GetPosts(int skip, int take);
        public Task<ServiceResponse<PostVM>> GetPost(int id);
        public Task<ServiceResponse<bool>> CreatePost(PostRequest request);
        public Task<ServiceResponse<bool>> DeletePost(int id);
        public Task<ServiceResponse<bool>> AddReaction(ReactionRequest request);

    }
}
