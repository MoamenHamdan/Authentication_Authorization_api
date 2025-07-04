using Authentication_Authorization_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services from Identity 
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDB>();


builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
    }
    );

//Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDB>(
    Options => Options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );
    

var app = builder.Build();

// Enable Swagger in *all* environments
app.UseSwagger();
app.UseSwaggerUI();

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app
    .MapGroup("/api")
    .MapIdentityApi<AppUser>();

app.MapPost("/api/signup",async (
    UserManager<AppUser> userManager,
    [FromBody] UserRegistrationModel usermodel
) =>
{
    AppUser user = new AppUser
    {
       
        Email = usermodel.Email,
        UserName = usermodel.Email,
        FullName = usermodel.FullName
    };
   var result = await userManager.CreateAsync(user,usermodel.Password);


    if (result.Succeeded)
    {
        return Results.Ok(new { message = "User registered successfully." });
    }
    else
    {
        return Results.BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }
});

app.Run();

public class UserRegistrationModel
{
    public string Password { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
}