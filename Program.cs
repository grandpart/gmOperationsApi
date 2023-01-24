using Microsoft.Extensions.Configuration;
using Zephry;

namespace Grandmark;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSingleton(new Connection
        {
            // use args here
            // --srv localhost --sch grandmark --usr zephman --pwd 1Password!
            DbServer = "localhost",
            DbName = "torq",
            DbUser = "zephman",
            DbPassword = "1Password!"
        });
        //builder.Services.AddAuthentication(options => options.DefaultScheme = "GrandmarkSchemeOptions")
        //    .AddScheme<GrandmarkSchemeOptions, GrandmarkAuthenticationHandler>("GrandmarkSchemeOptions", options => { });
        
        var app = builder.Build();
        app.UseCors(builder =>
        {
            builder
              .SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
        });
        app.UseHttpsRedirection();
        //app.UseRequestMiddleware(); // This is custom
        app.UseResponseMiddleware(); // This is custom
        app.MapControllers();

        app.Run();
    }
}