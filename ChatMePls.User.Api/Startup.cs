using System.IdentityModel.Tokens.Jwt;
using ChatMePls.User.Api.Application;
using ChatMePls.User.Api.JwtKey;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api;

public class Startup(IConfiguration config)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(
                    config.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(Startup).Assembly.FullName));
            });
        
        services.AddMediatR(mediatr =>
            mediatr.RegisterServicesFromAssembly(typeof(Program).Assembly));
        
        // Add services to the container.
        var symmetricKey = new SigningSymmetricKey("n3EdkTsYujuKYCfyiLy7XfwA4RoergFLZ5NuXCCZ");

        services.AddTransient<IJwtSigningEncodingKey>(_ => symmetricKey);
        services.AddTransient<JwtSecurityTokenHandler>();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricKey.GetKey(),
        
                    ValidateIssuer = true,
                    ValidIssuer = config.GetValue<string>("Jwt:Issuer"),
        
                    ValidateAudience = true,
                    ValidAudience = config.GetValue<string>("Jwt:Audience"),
        
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5),
                };
            });
        
        services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Stores.MaxLengthForKeys = 128;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.Configure<IdentityOptions>(io =>
        {
            io.Password.RequireDigit = false;
            io.Password.RequireLowercase = false;
            io.Password.RequireUppercase = false;
            io.Password.RequireNonAlphanumeric = false;
            io.Password.RequiredLength = 1;
            io.Password.RequiredUniqueChars = 0;
            io.SignIn.RequireConfirmedEmail = false;
            io.SignIn.RequireConfirmedPhoneNumber = false;
        });

        services.AddControllers();

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(ep =>
        {
            if (env.IsDevelopment())
            {
                ep.MapOpenApi();
            }
            
            ep.MapControllers();
        });
    }
}