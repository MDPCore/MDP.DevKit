using MDP.AspNetCore;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Builder
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureDefault(); // MDP.AspNetCore

            // App
            var app = builder.Build();
            app.MapDefaultControllerRoute();

            // Run
            app.Run();
        }
    }
}