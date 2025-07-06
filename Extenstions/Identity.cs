using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication_Authorization_api.Extenstions
{
    public static class Identity
    {
        public static void AddIdentityHandlerAndStores(this IServiceCollection services)
        {
            services
                .AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<AppDB>();
      
        }
        public static void ConfigureIdentityHandlerAndStores(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
