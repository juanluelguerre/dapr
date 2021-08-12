using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Samples.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // 1) - Integrate Dapr into the MVC pipeline and register Dapr client instance into the dependency injection container.
            services.AddControllers().AddDapr();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "subscriber", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "subscriber v1"));
            }
                        
            // app.UseHttpsRedirection();

            app.UseRouting();

            // 2 - B) It will unwrap the envelope for every incoming request with the application / cloudevents + json content - type.
            app.UseCloudEvents();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // 3) This is the Dapr subscribe handler.
                // It will automatically create an endpoint on dapr/subscribe that returns all topics that the application subscribes to (derived via the Topic attribute).
                // This is required for Dapr to know on what it has to subscribe.
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
            });
        }
    }
}
