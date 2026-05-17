using Microsoft.EntityFrameworkCore;
using ZenBlog.Domain.Entities;
namespace ZenBlog.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        /*
         Each DbSet<T> does three things simultaneously: it represents a table in your database,
         it's the entry point for all LINQ queries against that table,
         and it's the collection you add/remove entities from.
         */
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}