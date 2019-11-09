using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Models;

namespace SocialNetwork.Data.Database
{
    public class SocialNetworkContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SocialNetworkContext()
        {
        }

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SocialNetwork;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Post>()
                .HasOne(i => i.Author)
                .WithMany(i => i.Posts)
                .HasForeignKey(i => i.AuthorId);

            modelBuilder
                .Entity<Comment>()
                .HasOne(i => i.Author)
                .WithMany(i => i.Comments)
                .HasForeignKey(i => i.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Comment>()
                .HasOne(i => i.Post)
                .WithMany(i => i.Comments)
                .HasForeignKey(i => i.PostId);

            modelBuilder
                .Entity<Reaction>()
                .HasIndex(p => new { p.AuthorId, p.PostId }).IsUnique();

            modelBuilder
                .Entity<Reaction>()
                .HasOne(i => i.Post)
                .WithMany(i => i.Reactions)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Friend>()
                .HasKey(i => new { i.UserId, i.FriendId });

            modelBuilder
                .Entity<Friend>()
                .Property(i => i.UserId)
                .ValueGeneratedNever();

            modelBuilder
                .Entity<Friend>()
                .HasOne(i => i.User)
                .WithMany(i => i.Friends)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
