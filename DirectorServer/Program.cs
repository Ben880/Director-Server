using System;
using System.Threading;
using DirectorServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DirectorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Thread(new ThreadStart(StartUnityServer)).Start();
            Console.WriteLine("Starting web server");
            CreateHostBuilder(args).Build().Run();
        }

        public static void StartUnityServer()
        {
            Console.WriteLine("Starting Unity server");
            _ = new UnityConnection();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
