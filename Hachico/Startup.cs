using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hachico.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Hachico.Auth;
using Hachico.Hubs;

namespace Hachico
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
            var connection = @"Server=locationsatacusapi.database.windows.net,1433;Database=hachico;User ID=hoihoi4411;Password=thuydung@4411;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<HachicoContext>(options => options.UseSqlServer(connection));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".Hachico.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
            });
            services.AddSignalR();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSignalR(routes =>
            {
                routes.MapHub<LocationPet>("locationPet");
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "",
                defaults: new { controller = "swagger", action = "#" });
            });
            app.UseSession();
            app.UseMvc();
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost", "http://hachico4411.azurewebsites.net/api/authentication/AdminLogin")
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()); 
        }
    }
}
