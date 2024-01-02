using Library.Core.Implementations.Services;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;
using Library.Infrastructure.Repositories;

namespace Library.API.Extensions;

public static class ServiceConfigurations
{
    public static void AddServicesExtension(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}