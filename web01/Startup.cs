using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace web01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection _)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var connection = context.Features.Get<IHttpConnectionFeature>();
                    var backendInfo = new BackendInfo()
                    {
                        IP = connection.LocalIpAddress.ToString(),
                        Hostname = Dns.GetHostName(),
                    };

                    context.Response.ContentType = "application/json; charset=utf-8"
                    await JsonSerializer.SerializeAsync(context.Response.Body, backendInfo);
                });
            });
        }
    }
}
