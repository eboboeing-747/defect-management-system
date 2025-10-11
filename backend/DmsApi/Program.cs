using DmsDb;
using DmsDb.Repository;
using DmsDb.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DmsApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        string corsPolicy = "localCorsPolicy";

        builder.Services.AddCors(options =>
        {
        options.AddPolicy(
            name: corsPolicy,
            policy =>
            {
            policy
            .WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "http://127.0.0.1:5173",
                "https://127.0.0.1:5173",
                "http://26.186.13.168:5173",
                "https://26.186.13.168:5173"
            )
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DmsDbContext>(opt =>
        {
            opt.UseNpgsql(
                builder.Configuration.GetConnectionString(nameof(DmsDbContext))
                );
        });

        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<EstateObjectRepository>();

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<EstateObjectService>();
        builder.Services.AddScoped<FileService>();

        builder.Services.AddScoped(provider => { return new JwtOptions(configuration); });
        builder.Services.AddScoped<DmsDb.FileOptions>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtOptions.ReadConfigurationField(configuration, "SecretKey"))), RoleClaimType = "Role" };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["jwtToken"];

                        return Task.CompletedTask;
                    }
                };

            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(corsPolicy);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            FileService fileService = scope.ServiceProvider.GetRequiredService<FileService>();

            if (!fileService.CheckRootStorage())
            {
                Console.WriteLine("root store does not exit");
                return;
            }
        }

        app.Run();
    }
}
