using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.JwtKey;

internal interface IJwtSigningDecodingKey
{
    SecurityKey GetKey();
}