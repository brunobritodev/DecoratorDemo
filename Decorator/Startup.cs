using Decorator.Stores;
using Decorator.Stores.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Decorator
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
            services.AddMemoryCache();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Cache Strategy",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact()
                    {
                        Name = "Bruno Brito",
                        Email = "bhdebrito@gmail.com",
                        Url = new Uri("https://www.brunobrito.net.br")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/brunohbrito/RESTFul.API-Example/blob/master/LICENSE")
                    },

                });

            });
            RegisterServices(services);
            EnableDecorator(services);
        }

        private void EnableDecorator(IServiceCollection services)
        {
            services.Decorate<ICarStore, CarCachingStore>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICarStore, CarStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cache Strategy");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
