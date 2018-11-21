using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Persistence.BD;
using Persistence.BD.Repositories;

namespace Service
{
    public class BookService
    {
        public IEnumerable<Book> GetAllBooks()
        {
            IEnumerable<Book> books;
            using (UnityOfWork unity = new UnityOfWork())
            {
                books = new BookBdRepository(unity).GetAll();
                var subjectRep = new SubjectBdRepository(unity);
                var authorRep = new AuthorBdRepository(unity);
                foreach (var book in books)
                {
                    book.AddSubjects(subjectRep.GetSubjectsOfABook(book));
                    book.AddAuthors(authorRep.GetAuthorsOfABook(book));
                }

                unity.Complete();
            }

            return books;
        }

        public Book GetBookById(object id)
        {
            Book book = null;

            using (var unity = new UnityOfWork())
            {
                book = new BookBdRepository(unity).GetById(id);
                book.AddSubjects(new SubjectBdRepository(unity).GetSubjectsOfABook(book));
                book.AddAuthors(new AuthorBdRepository(unity).GetAuthorsOfABook(book));

                unity.Complete();
            }

            return book;

        }

        public IEnumerable<string> AddBook(Book book)
        {
            IEnumerable<string> errors = book.Validate();

            if (!errors.Any())
            {
                using (var unity = new UnityOfWork())
                {
                    new BookBdRepository(unity).Add(book);
                    unity.Complete();
                }
            }

            return errors;
        }

        public void RemoveBook(Book book)
        {
            using (var unity = new UnityOfWork())
            {
                new BookBdRepository(unity).Remove(book);
                unity.Complete();
            }
        }
    }
}
