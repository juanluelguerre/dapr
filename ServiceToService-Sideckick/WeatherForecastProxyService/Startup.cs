using Dapr.Client;
using Man.Dapr.Sidekick.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;

namespace WeatherForecastProxyService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Dapr Sidekick
            services.AddDaprSidekick(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecastProxyService", Version = "v1" });
            });

            bool useDaprInvoke = Configuration.GetValue("UseDaprInvoke", false);
            if (!useDaprInvoke)
            {
                // Issue: https://github.com/dapr/dotnet-sdk/issues/632                
                services.AddSingleton<IWeatherForecastClient, WeatherForecastClient>(
                    _ => new WeatherForecastClient(DaprClient.CreateInvokeHttpClient("backend")));
            }
            else
            {
                services.AddDaprClient();
                // var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
                // var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT") ?? "50000";
                // services.AddDaprClient(builder => builder
                //     .UseHttpEndpoint($"http://localhost:{daprHttpPort}")
                //     .UseGrpcEndpoint($"http://localhost:{daprGrpcPort}"));
                services.AddSingleton<IWeatherForecastClient, WeatherForecastInvokeClient>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecastProxyService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
