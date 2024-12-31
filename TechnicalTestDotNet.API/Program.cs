using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using TechnicalTestDotNet.API.Middleware;
using TechnicalTestDotNet.Core.Helpers;
using TechnicalTestDotNet.Core.Helpers.Token;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Auth;
//
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{    
    string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    var builder = WebApplication.CreateBuilder(args);

    // Configuración de JWT
    var jwtKey = builder.Configuration["Authentication:SecretKey"];
    var jwtIssuer = builder.Configuration["Authentication:Issuer"];

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

    builder.Services.AddAuthorization();

    builder.Services.AddScoped<TokenService>();
    builder.Services.AddScoped<UserRepository>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.AllowAnyOrigin()
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                          });
    });

    builder.Services.AddMvc();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.ExampleFilters();

        // Configuracion Swagger
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = builder.Configuration["SwaggerDoc:Version"],
            Title = builder.Configuration["SwaggerDoc:Title"],
            Description = builder.Configuration["SwaggerDoc:Description"],            
            Contact = new OpenApiContact
            {
                Name = "Contact the developer"
            },
            License = new OpenApiLicense
            {
                Name = "License",
            }
        });

        // Agregar información sobre seguridad
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
            new string[]{}
            }
        });
    });

    builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

    // Configuramos Login
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Configuramos AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    // Configure the DbContext
    builder.Services.AddSqlServer<dbContext>(builder.Configuration.GetConnectionString("dbContext"));

    // Add Dependency Injection for Repositories and other services
    builder.Services.RegisterDependencies();

    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });




    // Iniciamos App.
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseCors(MyAllowSpecificOrigins);

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    //app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Error inicializando program.cs");
    throw;
}
finally
{
    LogManager.Shutdown();
}