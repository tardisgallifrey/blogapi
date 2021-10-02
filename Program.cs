using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

//This code is boilerplate from dotnet new webapi
//We don't change anything here

namespace blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
