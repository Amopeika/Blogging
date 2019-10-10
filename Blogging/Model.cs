using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging.NewDb
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;")
            .EnableSensitiveDataLogging(true)
            .UseLoggerFactory(GetLoggerFactory());
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                    builder.AddConsole()
                            .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.FK_BlogId)
            .HasConstraintName("ForeignKey_Post_Blog");

            modelBuilder.Entity<Post>()
            .Property(p => p.CreateDate)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Post>()
            .HasIndex(p => p.Title)
            .HasName("Index_Title");


            modelBuilder.Entity<Blog>().HasData(
                new Blog { BlogId = 4, Url = "http://test1.dk" },
                new Blog { BlogId = 6, Url = "http://test3.dk" },
                new Blog { BlogId = 7, Url = "http://test4.dk" });

            modelBuilder.Entity<Post>().HasData(
                new Post() { PostId = 7, FK_BlogId = 4, Title = "Test 1 Post", Content = "Post Til Data Seeding Test" },
                new Post() { PostId = 8, FK_BlogId = 5, Title = "Test 2 Post", Content = "Post Til Data Seeding Test Ændring" },
                new Post() { PostId = 9, FK_BlogId = 6, Title = "Test 3 Post", Content = "Post Til Data Seeding Test" },
                new Post() { PostId = 10, FK_BlogId = 7, Title = "Test 4 Post", Content = "Post Til Data Seeding Test" });
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        [NotMapped]
        public string Category { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        [MaxLength(150)]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }

        public int FK_BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}