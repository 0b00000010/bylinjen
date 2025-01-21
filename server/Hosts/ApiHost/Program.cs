using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Com.SomeGameCorp.Bylinjen.Hosts.ApiHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.RegisterConfigurations(builder.Configuration);
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddCors();
        builder.Services.ConfigureAuthentication(builder.Configuration);
        builder.Host.UseSerilog((ctx, con) => con.ReadFrom.Configuration(ctx.Configuration));

        var app = builder.Build();
        if (!app.Environment.EnvironmentName.Equals("Hosted"))
        {
            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.MapOpenApi();
        }
        
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}