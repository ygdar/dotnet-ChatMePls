using ChatMePls.DefaultServices.Clients;
using ChatMePls.Profile.Client.Settings;
using ChatMePls.Profile.Contracts;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChatMePls.Profile.Client.Services;

public class ProfileClientServiceFactory(
    IOptions<ProfileServerSettings> profileServerSettings,
    ILoggerFactory loggerFactory
)
    : IClientServiceFactory<ProfileEndpoint.ProfileEndpointClient>
{
    public ProfileEndpoint.ProfileEndpointClient Create()
    {
        var settings = profileServerSettings.Value;

        var handler = new HttpClientHandler();

        var httpClient = new HttpClient(handler);

        var channelOptions = new GrpcChannelOptions
        {
            HttpClient = httpClient,
            LoggerFactory = loggerFactory
        };

        var channel = GrpcChannel.ForAddress(settings.ServerUrl, channelOptions);
        return new ProfileEndpoint.ProfileEndpointClient(channel);
    }
}