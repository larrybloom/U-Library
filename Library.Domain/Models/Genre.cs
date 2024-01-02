using Library.Domain.Interfaces;

namespace Library.Domain.Models;

public class Genre : IEntity, IAuditable
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Category Category { get; set; }
    public ICollection<Book> Books { get; set; }
}