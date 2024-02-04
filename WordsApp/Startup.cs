using Microsoft.OpenApi.Models;
using WordsApp.Authentication;
using WordsApp.Persistence;

namespace WordsApp;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(EasyAuthAuthenticationBuilderExtensions.EASYAUTHSCHEMENAME)
            .AddAzureContainerAppsEasyAuth();

        services.AddControllers();
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); })
            .AddHttpContextAccessor();

        AddAppServices(services);
    }

    private static void AddAppServices(IServiceCollection services)
    {
        services.AddAppUser()
                .AddPersistence();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}