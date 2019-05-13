using System;
using System.Threading.Tasks;
using EFCore.ScaffoldProcedures.EFCoreTypes;
using EFCore.ScaffoldProcedures.Models;

namespace EFCore.ScaffoldProcedures
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            using (var context = new BloggingContext())
            {
                await InsertTestData(context);

                // Generic version of ObjectParameter from EF6
                var outOverallCount = new OutputParameter<int>();
                // Programmability property to not have all the generated programmability methods
                // directly on the context object
                var posts = await context.Programmability.SpGetPostUrls(2, outOverallCount);

                Console.WriteLine($"Db contains {outOverallCount.Value} posts.");
                foreach (var post in posts)
                    Console.WriteLine(post.Url);
            }
        }

        private static async Task InsertTestData(BloggingContext context)
        {
            context.Blog.RemoveRange(context.Blog);
            context.Post.RemoveRange(context.Post);

            var blog1 = new Blog { Url = "http://blogs.msdn.com/adonet1" };
            var blog2 = new Blog { Url = "http://blogs.msdn.com/adonet2" };
            context.Blog.Add(blog1);
            context.Blog.Add(blog2);

            context.Post.Add(new Post { Blog = blog1, Title = "Post1" });
            context.Post.Add(new Post { Blog = blog2, Title = "Post2" });
            context.Post.Add(new Post { Blog = blog2, Title = "Post3" });
            await context.SaveChangesAsync();
        }
    }
}
