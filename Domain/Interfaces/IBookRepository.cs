using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetBookByTitle(string title);
        Book GetBookByIsbn(string isbn);
    }
}
