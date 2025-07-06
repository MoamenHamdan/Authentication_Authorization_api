using Authentication_Authorization_api.Extenstions;
using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddIdentityHandlerAndStores();

builder.Services.ConfigureIdentityHandlerAndStores();


// Configure EF Core
builder.Services.AddDbContext<AppDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);       

// JWT Auth Configuration
var jwtSecret = builder.Configuration["appsettings:JWTSecret"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!));

builder.Services.AddAuthentication(options =>
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("/api")
    .MapIdentityApi<AppUser>();

// Sign-Up Endpoint
app.MapPost("/api/signup", async (
    UserManager<AppUser> userManager,
    [FromBody] UserRegistrationModel userModel
) =>
{
    var user = new AppUser
    {
        Email = userModel.Email,
        UserName = userModel.Email,
        FullName = userModel.FullName
    };

    var result = await userManager.CreateAsync(user, userModel.Password);

    if (result.Succeeded)
    {
        return Results.Ok(new { message = "User registered successfully." });
    }
    return Results.BadRequest(new { errors = result.Errors.Select(e => e.Description) });
});

// Sign-In Endpoint
app.MapPost("/api/signin", async (
    UserManager<AppUser> userManager,
    [FromBody] LoginModel loginModel
) =>
{
    var user = await userManager.FindByEmailAsync(loginModel.Email);
    if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
    {
        var claims = new[]
        {
            new Claim("UserId", user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(10),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(securityToken);

        return Results.Ok(new
        {
            token = jwt
        });
    }

    return Results.BadRequest(new { message = "Invalid login attempt." });
});

app.Run();

// Models
public class UserRegistrationModel
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

public class LoginModel
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
