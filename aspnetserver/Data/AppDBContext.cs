using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext 
    {
        public DbSet<Post> Posts { get; set; }
     
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=react;Username=postgres;Password=123");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    Post[] postsToSeed = new Post[6];

        //    for (int i = 1; i < 6; i++)
        //    {
        //        postsToSeed[i - 1] = new Post
        //        {
        //            PostId = i,
        //            Title = $"Title post {i}",
        //            Content = $"Content post {i}"
        //        };
        //    }

        //    modelBuilder.Entity<Post>().HasData(postsToSeed);
        //}

    }
}
