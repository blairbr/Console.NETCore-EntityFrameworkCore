using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //in a web app this insert would be tied to a method called to an onclick event 
            // Disconnected Student entity
            Blog blog1 = new Blog() { Url = "http://url.com" };

            blog1.Rating = 10;

            using (var context = new BloggingContext())
            {
                context.Update<Blog>(blog1);

                // or the followings are also valid
                // context.Blog.Update(blog1);
                // context.Attach<Blog>(blog1).State = EntityState.Modified;
                // context.Entry<Blog>(blog1).State = EntityState.Modified; 

                context.SaveChanges();
            }
        }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CodeFirstNewDatabaseSample;Trusted_Connection=True");
              //  @"Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True");
        }

}

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }

}

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
