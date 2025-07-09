using Microsoft.AspNetCore.Authorization;

namespace Authentication_Authorization_api.Extensions
{


    public static class AccountEndPoints
    {

        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", UserProfile).RequireAuthorization();

            return app;
        }
        //or putting this 
        [Authorize]
        private static string UserProfile()
        {

            return "User Profile";
        }

    }

}