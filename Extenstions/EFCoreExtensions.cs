using Authentication_Authorization_api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication_Authorization_api.Extenstions
{
    public static class EFCoreExtensions
    {

        public static IServiceCollection InjectDbContext(
             this IServiceCollection services,
             IConfiguration config)
        {
            services.AddDbContext<AppDB>(options =>
                     options.UseSqlServer(config.GetConnectionString("DevDB")));
            return services;
        }
    }
}