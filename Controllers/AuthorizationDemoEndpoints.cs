using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_Authorization_api.Controllers
{


    public static class AuthorizationDemoEndpoints
{

    public static IEndpointRouteBuilder MapAuthorizationDemoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/AdminOnly", AdminOnly);


        app.MapGet("/AdminOrTeacher", [Authorize(Roles = "Admin,Teacher")] () =>
        { return "Admin Or Teacher"; });

        app.MapGet("/LibraryMembersOnly", [Authorize(Policy = "HasLibraryId")] () =>
        { return "Library members only"; });

        app.MapGet("/ApplyForMaternityLeave", [Authorize(Roles = "Teacher", Policy = "FemalesOnly")] () =>
        { return "Applied for maternity leave."; });

        app.MapGet("/Under10sAndFemale",
        [Authorize(Policy = "Under10")]
        [Authorize(Policy = "FemalesOnly")] () =>
        { return "Under 10 And Female"; });

        return app;
    }

    [Authorize(Roles = "Admin")]
    private static string AdminOnly()
    { return "Admin Only"; }
}
}