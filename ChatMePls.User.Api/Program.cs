using System.IdentityModel.Tokens.Jwt;
using ChatMePls.User.Api.Authentication;
using ChatMePls.User.Api.JwtKey;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IJwtSigningEncodingKey>(provider => new SigningSymmetricKey("n3EdkTsYujuKYCfyiLy7XfwA4RoergFLZ5NuXCCZ"));
builder.Services.AddTransient<JwtSecurityTokenHandler>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();