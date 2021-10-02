using Microsoft.EntityFrameworkCore;

//See comments in BlogContext

public class BlogContext : DbContext
    {
        public BlogContext (DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<blog.BlogEntry> BlogEntry { get; set; }
    }
