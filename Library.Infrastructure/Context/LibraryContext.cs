using System.Reflection;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library.Infrastructure.Context;

public class LibraryContext : IdentityDbContext<User>
{
    private const string UpdatedAt = "UpdatedAt";
    private const string CreatedAt = "CreatedAt";

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    public LibraryContext(DbContextOptions<LibraryContext> options): base(options)
    {
    }

    protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is Property appUser)
            {
                AuditPropertiesChange(item.State, appUser.GetType());
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
       

    private static void AuditPropertiesChange<T>(EntityState state, T obj) where T : class
    {
        PropertyInfo? value;
        switch (state)
        {
            case EntityState.Modified:
                value = obj.GetType().GetProperty(UpdatedAt);
                value?.SetValue(obj, DateTime.UtcNow);
                break;
            case EntityState.Added:
                value = obj.GetType().GetProperty(CreatedAt);
                value?.SetValue(obj, DateTime.UtcNow);
                value = obj.GetType().GetProperty(UpdatedAt);
                value?.SetValue(obj, DateTime.UtcNow);
                break;
            default:
                break;
        }
    }
}