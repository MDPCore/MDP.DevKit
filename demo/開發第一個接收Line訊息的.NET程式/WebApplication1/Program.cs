using MDP.AspNetCore;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Builder
            var builder = WebApplication.CreateBuilder(args);
            builder.AddMdp(); // 掛載MDP

            // App
            var app = builder.Build();
            app.MapDefaultControllerRoute();

            // Run
            app.Run();
        }
    }
}