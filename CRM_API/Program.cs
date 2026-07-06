using Business_Layer.Interfaces;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.MasterIInterface;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services.AuditLog;
using Business_Layer.Services.Auth;
using Business_Layer.Services.CommonServices;
using Business_Layer.Services.MenuServices;
using BusinessLayer.Services;
using CRM_API.Middleware;
using DataAccess_Layers.Data;
using DataAccess_Layers.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Settings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// ======================================================
// SERILOG CONFIGURATION
// ======================================================

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


// ======================================================
// CONTROLLERS
// ======================================================

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// ======================================================
// DATABASE CONTEXT
// ======================================================

builder.Services.AddDbContext<CRMContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("DefaultConnection"));
});


// ======================================================
// UNIT OF WORK
// ======================================================

builder.Services.AddScoped<
    IUnitOfWork,
    UnitOfWork>();

// ======================================================
// AUTH SERVICE (ADD THIS HERE)
// ======================================================

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAuditService, AuditService>();

// ======================================================
// COMMON UserId  (ADD THIS HERE)
// ======================================================

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();


// ======================================================
// JWT CONFIGURATION
// ======================================================

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSection =
    builder.Configuration.GetSection("JwtSettings");

var secretKey =
    jwtSection["SecretKey"];


// ======================================================
// JWT AUTHENTICATION
// ======================================================

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)),

        ClockSkew = TimeSpan.Zero
    };
});


// ======================================================
// SWAGGER CONFIGURATION
// ======================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "CRM API",
            Version = "v1",
            Description = "CRM API Documentation"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT Token. Example: Bearer eyJhbGciOi..."
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});


// ======================================================
// BUILD APPLICATION
// ======================================================

var app = builder.Build();


// ======================================================
// GLOBAL EXCEPTION MIDDLEWARE
// ======================================================

app.UseMiddleware<ExceptionMiddleware>();


// ======================================================
// SWAGGER MIDDLEWARE
// ======================================================

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "CRM API V1");

    options.RoutePrefix = string.Empty;
});


// ======================================================
// REQUEST / RESPONSE LOGGER
// ======================================================

app.UseMiddleware<ExecutionTimeMiddleware>();
app.UseMiddleware<RequestResponseMiddleware>();




// ======================================================
// HTTPS
// ======================================================

app.UseHttpsRedirection();

app.UseCors("AllowAngular");


// ======================================================
// JWT AUTHENTICATION
// ======================================================

app.UseAuthentication();

app.UseAuthorization();


// ======================================================
// MAP CONTROLLERS
// ======================================================

app.MapControllers();


// ======================================================
// RUN APPLICATION
// ======================================================

app.Run();