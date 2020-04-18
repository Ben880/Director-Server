using System;
using DirectorServer.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DirectorServer
{
    public class Startup
    {
        public static IConnectionManager ConnectionManager;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
           
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                
            });
            app.UseStaticFiles();
            app.UseSignalR(config => {
                config.MapHub<MessageHub>("/messages");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Got a non null refrence");
                var hubContext = context.RequestServices
                    .GetRequiredService<Microsoft.AspNetCore.SignalR.IHubContext<MessageHub>>();

                if (hubContext != null)
                {
                    HubContextHolder.setContext(hubContext);
                    Console.WriteLine("Got a non null refrence");
                }
                else
                {
                    Console.WriteLine("injection returned null refrence");
                }
            });
     
            
            //HubContextHolder.setContext(GlobalHost.ConnectionManager.GetConnectionContext<MessageHub>());
            //HubContextHolder.setContext(GlobalHost.ConnectionManager.GetHubContext<MessageHub>());
            /*IHubContext context = Startup.ConnectionManager.GetHubContext<(IHub) MessageHub>();
            context.Clients.All.someMethod();
            GlobalHost.ConnectionManager*/
            
            
        }
    }
}
