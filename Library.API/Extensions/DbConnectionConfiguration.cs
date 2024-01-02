using Library.Domain.Models;
using Library.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Extensions;

public static class DbConnectionConfiguration
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env)
    {
        services.AddDbContext<LibraryContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Library.Infrastructure"));
        });

        var builder = services.AddIdentity<User, IdentityRole>(x =>
        {
            x.Password.RequiredLength = 8;
            x.Password.RequireDigit = false;
            x.Password.RequireUppercase = true;
            x.Password.RequireLowercase = true;
            x.User.RequireUniqueEmail = true;
            x.SignIn.RequireConfirmedEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
        _ = builder.AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
    }
}