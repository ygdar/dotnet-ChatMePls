using ChatMePls.Profile.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChatMePls.Gateway;

public class Startup(IConfiguration config)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        ProfileClientModule.Register(services, config);

        services.AddControllers();

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(ep =>
        {
            if (env.IsDevelopment())
            {
                ep.MapOpenApi();
            }
            
            ep.MapControllers();
        });
    }
}