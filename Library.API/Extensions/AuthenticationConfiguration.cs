using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Library.API.Extensions;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration config,
        IHostEnvironment env)
    {
        var jwt = new
        {
            Issuer = "",
            Audience = "",
            Token = config.GetSection("Jwt:Token").Value,
        };

        services.AddAuthentication(v =>
            {
                v.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                v.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(v =>
            {
                v.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwt.Issuer != null,
                    ValidateLifetime = true,
                    ValidateAudience = jwt.Audience != null,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer ?? null,
                    ValidAudience = jwt.Audience ?? null,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Token ?? string.Empty))
                };
            });
    }
}