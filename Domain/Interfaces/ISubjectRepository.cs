using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjectsOfABook(Book book);
    }
}
