using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models.Domain;

namespace TestWebApplication.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogDBContext blogDBContext;

        public BlogPostCommentRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogDBContext.BlogPostsComment.AddAsync(blogPostComment);
            await blogDBContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await blogDBContext.BlogPostsComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
