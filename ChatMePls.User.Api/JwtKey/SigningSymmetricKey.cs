using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.JwtKey;

internal class SigningSymmetricKey(string secretKey)
    : IJwtSigningEncodingKey, IJwtSigningDecodingKey
{
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(secretKey));
    
    public string SigningAlgorithm => SecurityAlgorithms.HmacSha256;

    public SecurityKey GetKey()
    {
        return _key;
    }
}