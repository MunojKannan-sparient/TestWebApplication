
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models.Domain;

namespace TestWebApplication.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDBContext blogDBContext;

        public BlogPostLikeRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await blogDBContext.BlogPostsLike.CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogDBContext.BlogPostsLike.AddAsync(blogPostLike);
            await blogDBContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await blogDBContext.BlogPostsLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
