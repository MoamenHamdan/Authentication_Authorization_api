using System.Security.Claims;
using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
        private static async Task<IResult> UserProfile(ClaimsPrincipal user, UserManager<AppUser> userManager)
        {
            string userID = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userManager.FindByIdAsync(userID);

            return Results.Ok(
                new
                {
                    Email = userDetails?.Email,
                    FullName = userDetails?.FullName
                    
                }

            );
            }

    }

}