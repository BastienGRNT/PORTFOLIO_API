using System.Security.Cryptography.X509Certificates;
using API.Config;
using API.Config.Sql;
using static API.Config.EnvVariable;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load();
        CheckVariable.Run();
        MigrateSql.Run();
        
        var builder = WebApplication.CreateBuilder(args);

        if (environment == "PROD")
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(portHttp);
                serverOptions.ListenAnyIP(portHttps, listenOptions =>
                {
                    var certificate = X509Certificate2.CreateFromPemFile(pathCertPem, pathKeyPem);
                    listenOptions.UseHttps(certificate);
                });
            });
        }
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        var app = builder.Build();

        if (environment == "DEV")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}