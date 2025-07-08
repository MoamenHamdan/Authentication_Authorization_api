using Authentication_Authorization_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_api.Extenstions
{
    public static class EFCoreExtensions
    {

        public static IServiceCollection InjectDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDB>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
    
}
