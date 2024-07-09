using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models.Domain;

namespace TestWebApplication.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDBContext blogDBContext;

        public BlogPostRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDBContext.AddAsync(blogPost);
            await blogDBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await blogDBContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                blogDBContext.BlogPosts.Remove(existingBlog);
                await blogDBContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogDBContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await blogDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await blogDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.Author = blogPost.Author;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;
                await blogDBContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(y => y.UrlHandle == urlHandle);
        }
    }
}
