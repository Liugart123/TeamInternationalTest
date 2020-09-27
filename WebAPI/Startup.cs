using Core.Services;
using Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.Repositories;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Auth;

namespace WebAPI
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
            RegisterDependencies(services);
            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API V1");
            });
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            //Dependencies injections
            services.AddSingleton<IOperationServices, OperationServices>();
            services.AddSingleton<IDataRepository, MongoDbRespository>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Restaurant {groupName}",
                    Version = groupName,
                    Description = "Restaurant API",
                    Contact = new OpenApiContact
                    {
                        Name = "Restaurant Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });

                options.OperationFilter<SwaggerCustomHeaders>();
            });
        }
    }
}
