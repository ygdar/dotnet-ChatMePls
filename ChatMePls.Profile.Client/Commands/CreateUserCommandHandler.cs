using System.Security.Cryptography.X509Certificates;
using ChatMePls.DefaultServices.Clients;
using ChatMePls.DefaultServices.CQRS;
using ChatMePls.Profile.Client.Settings;
using ChatMePls.Profile.Contracts;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChatMePls.Profile.Client.Commands;

public record CreateUserCommand(string Email, string Username, string Password)
    : ICommand<CreateUserResult>;

public record CreateUserResult(bool Success, string[] Errors);

public class CreateUserCommandHandler(
    IClientServiceFactory<ProfileEndpoint.ProfileEndpointClient> grpcClientFactory)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var client = grpcClientFactory.Create();

        var query = new CreateUserRequest
        {
            UserName = request.Username,
            Email = request.Email,
            Password = request.Password,
        };
        var response = await client.CreateUserAsync(query, new CallOptions());
        
        return new CreateUserResult(response.Success, response.Errors.ToArray());
    }
}