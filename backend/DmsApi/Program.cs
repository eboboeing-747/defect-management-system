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

        // Add services to the container.

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

        builder.Services.AddScoped<UserService>();

        builder.Services.AddScoped(provider => { return new JwtOptions(configuration); });

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
                        Encoding.UTF8.GetBytes(JwtOptions.ReadConfigurationField(configuration, "SecretKey"))
                    )
                };
            });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.Use(async(context, next) => {
        //     Console.WriteLine($"[middleware logger] {context}");
        //
        //     await next.Invoke();
        // });

        app.MapControllers();

        app.Run();
    }
}
