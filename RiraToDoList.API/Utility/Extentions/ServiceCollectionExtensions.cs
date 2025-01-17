using Microsoft.EntityFrameworkCore;
using RiraToDoList.Domain;
using RiraToDoList.Domain.LogService.@interface;
using RiraToDoList.Infrastructure.Helper.LogService;
using RiraToDoList.Infrastructure.Persistence;
using RiraToDoList.Infrastructure.Persistence.Context;
using RiraToDoList.Service.Implimentation.MappingProfiles;
using RiraToDoList.Service.Implimentation.Service.Implimentations;
using RiraToDoList.Service.Service.Interfaces;

namespace RiraToDoList.API.Utility.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, string databaseConnectionString)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.RegisterDatabase(databaseConnectionString);
            services.RegisterMapper();
        }


        private static void RegisterDatabase(this IServiceCollection services, string databaseConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(databaseConnectionString));
        }
        private static void RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TaskToTaskDetailDtoProfile).Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }


    }
}
