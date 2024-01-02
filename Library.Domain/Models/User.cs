using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Models;

public class User: IdentityUser, IEntity, IAuditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

   public IEnumerable<Book> Books { get; set; }
}