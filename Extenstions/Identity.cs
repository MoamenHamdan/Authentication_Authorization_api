using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication_Authorization_api.Extenstions
{
    public static class Identity
    {
        public static IServiceCollection AddIdentityHandlerAndStores(this IServiceCollection services)
        {
            services
                .AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<AppDB>();
            return services;

      
        }
        public static IServiceCollection ConfigureIdentityHandlerAndStores(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }

        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration config)
        {

            // JWT Auth Configuration
            var jwtSecret = config["appsettings:JWTSecret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static WebApplication AddIdentityMiddleWares(this WebApplication app)
        {

            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
