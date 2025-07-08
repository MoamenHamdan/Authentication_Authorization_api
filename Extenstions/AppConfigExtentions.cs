using Authentication_Authorization_api.Models;

namespace Authentication_Authorization_api.Extenstions
{
    public static class AppConfigExtentions
    {

  
            public static WebApplication ConfigureCORS(
                this WebApplication app,
                IConfiguration config)
            {
                app.UseCors();
                return app;
            }

            public static IServiceCollection AddAppConfig(
                this IServiceCollection services,
                IConfiguration config)
            {
                services.Configure<AppSettings>(config.GetSection("AppSettings"));
                return services;
            }
        }
    }