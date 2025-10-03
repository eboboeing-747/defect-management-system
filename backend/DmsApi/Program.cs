using DmsDb;
using DmsDb.Repository;
using DmsDb.Service;
using Microsoft.EntityFrameworkCore;

namespace DmsApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<DmsDbContext>(opt =>
        {
            opt.UseNpgsql(
                builder.Configuration.GetConnectionString(nameof(DmsDbContext))
            );
        });

        builder.Services.AddScoped<UserRepository>();

        builder.Services.AddScoped<UserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
