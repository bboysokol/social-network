using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Data.Database
{
    class SocialNetworkContextPartial
    {
        public static SocialNetworkContext Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<SocialNetworkContext>().UseSqlServer(connectionString);
            var options = builder.Options;
            return new SocialNetworkContext(options);
        }
    }
}
