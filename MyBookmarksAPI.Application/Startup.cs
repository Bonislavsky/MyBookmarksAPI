using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.DAL.Repository;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Helpers.Mapping;
using MyBookmarksAPI.Service;
using MyBookmarksAPI.Service.Implementation;
using MyBookmarksAPI.Service.Interface;
using System;
using System.IO;
using System.Reflection;

namespace MyBookmarksAPI
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
            var connection = Configuration.GetConnectionString("MyBookMarksConnection");
            services.AddDbContext<MyBookmarksDbContext>(options => options.UseSqlServer(connection));

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyBookmarksAPI",
                    Description = "An ASP.NET Core Web API for managing MyBookmark items",
                    Contact = new OpenApiContact
                    { 
                        Name = "Sergey Bonislavsky", 
                        Email = "bonislavskys@gmail.com", 
                        Url = new Uri("https://www.linkedin.com/in/sergey-bonislavsky/") 
                    },
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(typeof(AppMappingProfileUser));
            services.AddAutoMapper(typeof(AppMappingProfileFolder));
            services.AddAutoMapper(typeof(AppMappingProfileBookmark));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IBookmarkService, BookmarkService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBookmarksAPI v1"));
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
