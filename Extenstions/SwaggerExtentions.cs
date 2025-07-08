using Authentication_Authorization_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_api.Extenstions
{
    public static class SwaggerExtentions
    {
        public static IServiceCollection AddSwaggerExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication ConfigSwaggerExplorer(this WebApplication app)
        {
            if (app.Environment.IsDevelopment()) { 
            app.UseSwagger();
            app.UseSwaggerUI();
        }
            return app;
        }


    }
}
