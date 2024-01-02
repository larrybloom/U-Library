using Library.Domain.Enums;

namespace Library.API.Extensions;

public static class AuthorizationConfiguration
{
    public static void AddAuthorizationExtension(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", policy => policy.RequireRole(UserRole.User.ToString()));
            options.AddPolicy("Admin", policy => policy.RequireRole(UserRole.Admin.ToString()));
        });
    }
}