using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatMePls.User.Api.JwtKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.Authentication.UserAuth;

public record AuthUserRequest(string UserName, string Password);

public record AuthUserResponse(string token);

[ApiController]
[Route("[controller]")]
public class UserAuthEndpoint(
    IJwtSigningEncodingKey signingEncodingKey,
    JwtSecurityTokenHandler jwtTokenHandler)
    : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public IActionResult AuthUser(AuthUserRequest request)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, request.UserName),
        };

        var token = jwtTokenHandler.CreateJwtSecurityToken(
            issuer: GetType().Assembly.GetName().Name,
            audience: "Client",
            subject: new ClaimsIdentity(claims),
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(5),
            issuedAt: DateTime.Now,
            signingCredentials: new SigningCredentials(
                signingEncodingKey.GetKey(),
                signingEncodingKey.SigningAlgorithm)
            );
        
        var jwt = jwtTokenHandler.WriteToken(token);
        
        return Ok(new AuthUserResponse(jwt));
    }
}