using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthorsOfABook(Book book);
    }
}
