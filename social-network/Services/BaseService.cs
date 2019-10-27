using SocialNetwork_Backend.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Services
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
