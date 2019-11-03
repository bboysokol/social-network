using SocialNetwork.Api.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services
{
    
    public class BaseService
    {       
        public BaseService(SocialNetworkContext context)
        {
            Context = context;
        }
        
        protected SocialNetworkContext Context { get; }
    }
}
