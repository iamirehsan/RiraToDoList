
using Microsoft.EntityFrameworkCore;
using RiraToDoList.API.Utility.Extentions;
using RiraToDoList.Infrastructure.Persistence.Context;

namespace RiraToDoList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();


            var databaseConnectionString = builder.Configuration.GetConnectionString("SqlConnection");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterServices(databaseConnectionString);

            var app = builder.Build().SeedDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
