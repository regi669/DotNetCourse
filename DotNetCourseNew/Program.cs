using System.Text;
using DotNetCourseNew.Authorization;
using DotNetCourseNew.Authorization.Resource;
using DotNetCourseNew.Configuration;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Middleware;
using DotNetCourseNew.Models;
using DotNetCourseNew.Models.Validators;
using DotNetCourseNew.Services;
using DotNetCourseNew.Services.Implementation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddNLog();
// Add services to the container.
var authConfig = new AuthConfig();
builder.Configuration.GetSection("Authentication").Bind(authConfig);

builder.Services.AddSingleton(authConfig);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authConfig.JWTIssuer,
        ValidAudience = authConfig.JWTIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JWTKey)),
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PolandNationality", b => b.RequireClaim("Nationality", "Poland"));
    options.AddPolicy("AtLeast20YearsOld", b => b.AddRequirements(new MinimumAgeRequirement(20)));
    options.AddPolicy("AtLeast2RCreated", b => b.AddRequirements(new MinimumRestaurantsCreated(2)));
});
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumRestaurantsCreatedHandler>();


builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDTOValidator>();
builder.Services.AddScoped<IValidator<RestaurantQuery>, RestaurantQueryValidator>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", corsBuilder =>
        corsBuilder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
    );
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontEndClient");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();