using ChatMePls.Profile.Contracts;
using Grpc.Core;

namespace ChatMePls.Profile.Service.Endpoints;

public class ProfileEndpoint : Contracts.ProfileEndpoint.ProfileEndpointBase
{
    public override Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CreateUserResponse
        {
            Success = true,
            Errors = {  }
        });
    }

    public override Task<GetTokenResponse> GetToken(GetTokenRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetTokenResponse
        {
            Token = Guid.NewGuid().ToString()
        });
    }
}