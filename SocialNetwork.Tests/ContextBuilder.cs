using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Database;

namespace SocialNetwork.Tests
{
    public class ContextBuilder
    {
        private readonly DbContextOptionsBuilder<SocialNetworkContext> builder;
        private readonly DbContextOptions<SocialNetworkContext> options;

        public SocialNetworkContext Context { get; }

        public ContextBuilder()
        {
            builder = new DbContextOptionsBuilder<SocialNetworkContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SocialNetwork;Trusted_Connection=True;MultipleActiveResultSets=true");
            options = builder.Options;
            Context = new SocialNetworkContext(options);
        }
    }
}
