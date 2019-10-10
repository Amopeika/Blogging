using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blogging.NewDb
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using (var db = new BloggingContext())
            {
                //db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(" - {0}", blog.Url);
                }
            }*/
            //InsertBlog();
            //InsertMultipleBlogs();
            //LoadingAllData();
            //LoadingSingleEntity();
            //GetFirstOrDefault();
            //GetLastOrDefault();
            //Filtering();
            //FilteringLike();
            //NonParameterizedQuery();
            //ParameterizedQuery();
            //ObjectFindByKeyValue();
            CreateRelatedData();
            //IncludeChildEntity();
        }

        #region INSERT
        private static void InsertBlog()
        {
            // Contexten har også en Add() metode og kan selv mappe objektet ind på den korrekte DbSet.
            using (var context = new BloggingContext())
            {
                context.Blog.Add(new Blog { Url = "http://dotnet.com" });
                //context.Add(new Blog { Url = "http://dotnet.com" });
                context.SaveChanges();
            }
        }

        private static void InsertMultipleBlogs()
        {
            using (var context = new BloggingContext())
            {
                Blog blog1 = new Blog { Url = "http://itdata.net" };
                Blog blog2 = new Blog { Url = "http://abctv.dk" };
                context.Blog.AddRange(blog1, blog2);
                context.SaveChanges();
            }
        }
        #endregion

        #region QUERIES SINGLE ENTITY
        //tolist så henter den det hele 
        private static void LoadingAllData()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blog;
            }
        }
        //Smider en exception hvis der er mere end 1 object
        private static void LoadingSingleEntity()
        {
            using (var context = new BloggingContext())
            {
                var blog = context.Blog
                    .Single(b => b.BlogId == 1);
            }
        }
        //Smider NULL hvis der ikke findes sådan et object
        private static void GetFirstOrDefault()
        {
            using (var context = new BloggingContext())
            {
                string url = "http://dotnet.com";
                context.Blog.FirstOrDefault(u => u.Url == url);
            }
        }

        private static void GetLastOrDefault()
        {
            // Bemærk at hvis man undlader OrderBy vil hele collectionen af Blogs læses ind i memory og bliver sorteret der.
            using (var context = new BloggingContext())
            {
                string url = "http://dotnet.com";
                context.Blog.OrderBy(b => b.Url).LastOrDefault(u => u.Url == url);
            }
        }
        private static void Filtering()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blog
                    .Where(b => b.Url.Contains("dotnet"))
                    .ToList();
            }
        }
        private static void FilteringLike()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blog
                    .Where(b => EF.Functions.Like(b.Url, "dot%"))
                    .ToList();
            }
        }

        private static void NonParameterizedQuery()
        {
            // Bemærk at når man sammenligner med en fast værdi så benyttes der ikke en parameter i SQL-querien
            using (var context = new BloggingContext())
            {
                var blogs = context.Blog.Where(s => s.Url == "http://dotnet.com").ToList();
            }
        }

        private static void ParameterizedQuery()
        {
            // Bemærk at når man sammenligner med en variabel så benyttes der en parameter i SQL-querien
            using (var context = new BloggingContext())
            {
                string url = "http://dotnet.com";
                var blogs = context.Blog.Where(s => s.Url == url).ToList();
            }
        }

        private static void ObjectFindByKeyValue()
        {
            // Bemærk at Find() ikke er en LINQ metode, men bor i DbSet<T> klassen. Læs dens beskrivelse...
            using (var context = new BloggingContext())
            {
                //finder en entity med given primary key. Hvis den er tracked af context så laver den ikke en request til databasen men returner det med det samme.
                //ellers laver den en request og attacher det til context og vis ingenting er fundet returner NULL
                context.Blog.Find(1);
            }
        }
        #endregion

        private static void CreateRelatedData()
        {
            using (var context = new BloggingContext())
            {
                Post post1 = new Post { Title = "Post 1", Content = "abc", FK_BlogId = 1 };
                Post post2 = new Post { Title = "Post 2", Content = "def", FK_BlogId = 1 };
                Post post3 = new Post { Title = "Post 3", Content = "hij", FK_BlogId = 1 };
                Post post4 = new Post { Title = "Post 4", Content = "DateTest", FK_BlogId = 1 };
                context.Post.AddRange(post1, post2, post3, post4);
                context.SaveChanges();
            }
        }

        private static void IncludeChildEntity()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blog
                    .Include(blog => blog.Posts)
                    .ToList();
                foreach (Blog blog in blogs)
                {
                    Console.WriteLine(blog.Url);
                    foreach (Post post in blog.Posts)
                    {
                        Console.WriteLine(post.Content);
                    }
                }
            }
        }
    }
}