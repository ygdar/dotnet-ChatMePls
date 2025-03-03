using ChatMePls.DefaultServices.Clients;
using ChatMePls.Profile.Client.Services;
using ChatMePls.Profile.Client.Settings;
using ChatMePls.Profile.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatMePls.Profile.Client;

public class ProfileClientModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(mediatr =>
            mediatr.RegisterServicesFromAssembly(typeof(ProfileClientModule).Assembly));
        
        services.Configure<ProfileServerSettings>(configuration.GetSection("Services:Profile"));

        services
            .AddTransient<IClientServiceFactory<ProfileEndpoint.ProfileEndpointClient>, ProfileClientServiceFactory>();
    }
}