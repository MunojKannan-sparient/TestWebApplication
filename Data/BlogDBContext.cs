using Microsoft.EntityFrameworkCore;
using TestWebApplication.Models.Domain;

namespace TestWebApplication.Data
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostsLike { get; set; }
        public DbSet<BlogPostComment> BlogPostsComment { get; set; }
    }
}
