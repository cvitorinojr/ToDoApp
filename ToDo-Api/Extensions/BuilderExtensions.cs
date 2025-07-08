using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo_Repository.Context;

namespace ToDo_Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddDatabase(this WebApplicationBuilder builder) =>
                builder.Services.AddDbContext<DefaultContext>(options =>
                    options.UseInMemoryDatabase("ToDoDb"));

        public static void AddCorsConfiguration(this WebApplicationBuilder builder) => builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        public static void AddSwaggerConfiguration(this WebApplicationBuilder builder) => builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("0.0.1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "0.0.1",
                    Description = "API ToDo App",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "TodoApp" }
                });
        });
    }
    
}