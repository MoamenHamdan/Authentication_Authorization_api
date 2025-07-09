using Authentication_Authorization_api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication_Authorization_api.Extensions
{
    public static class EFCoreExtensions
    {

        public static IServiceCollection InjectDbContext(
this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDB>(options =>options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}