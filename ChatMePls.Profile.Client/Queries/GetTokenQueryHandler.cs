using ChatMePls.DefaultServices.Clients;
using ChatMePls.DefaultServices.CQRS;
using ChatMePls.Profile.Contracts;
using Grpc.Core;

namespace ChatMePls.Profile.Client.Queries;

public record GetTokenQueryRequest(string Username, string Password)
    : IQuery<GetTokenQueryResponse>;

public record GetTokenQueryResponse(string AccessToken);

public class GetTokenQueryHandler(
    IClientServiceFactory<ProfileEndpoint.ProfileEndpointClient> grpcClientFactory
)
    : IQueryHandler<GetTokenQueryRequest, GetTokenQueryResponse>
{
    public async Task<GetTokenQueryResponse> Handle(GetTokenQueryRequest request, CancellationToken cancellationToken)
    {
        var client = grpcClientFactory.Create();
        
        var query = new GetTokenRequest
        {
            UserName = request.Username,
            Password = request.Password
        };
        var response = await client.GetTokenAsync(query, new CallOptions());
        return new GetTokenQueryResponse(response.Token);
    }
}