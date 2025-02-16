using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.JwtKey;

public interface IJwtSigningEncodingKey
{
    string SigningAlgorithm { get; }

    SecurityKey GetKey();
}