using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public class BookBdRepository : BaseBdRepository<Book>, IBookRepository
    {
        private Book ReadBook(DbDataReader reader)
        {
            int bookId = (int)reader["Id"];
            string title = reader["Title"].ToString();
            string isbn = reader["Isbn"].ToString();
            int edition = (int)reader["Edition"];
            string year = reader["Year"].ToString();

            return new Book(title, isbn, edition, year) { Id = bookId };
        }
        private IEnumerable<Book> GetBooks(string sql)
        {
            List<Book> books = new List<Book>();

            using (var command = CreateCommand(sql))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(ReadBook(reader));
                    }
                }
            }

            var subjectRep = new SubjectBdRepository();
            var authorRep = new AuthorBdRepository();

            foreach (var book in books)
            {
                book.AddSubjects(subjectRep.GetSubjectsOfABook(book));
                book.AddAuthors(authorRep.GetAuthorsOfABook(book));
            }

            subjectRep.Dispose();
            authorRep.Dispose();

            return books;
        }
        public override Book GetById(object id) => GetBooks("Select * from Books Where Id = @Id").FirstOrDefault();
        public override IEnumerable<Book> GetAll() => GetBooks("Select * from Books");
        public override void Add(Book book)
        {
            string bookSql = "INSERT INTO books (Title, ISBN, Year, Edition) VALUES (@Title, @Isbn, @Year, @Edition); SELECT MAX(Id) FROM books";
            string subjectSql = "INSERT INTO book_subject (BookId, SubjectId) VALUES (@BookId, @SubjectId)";
            string authorSql = "INSERT INTO book_author (BookId, AuthorId) VALUES (@BookId, @AuthorId)";

            DbParameter titleParam = CreateParameter(DbType.String, "@Title", book.Title);
            DbParameter isbnParam = CreateParameter(DbType.String, "@Isbn", book.Isbn);
            DbParameter yearParam = CreateParameter(DbType.String, "@Year", book.Year);
            DbParameter editionParam = CreateParameter(DbType.Int32, "@Edition", book.Edition);


            using (var bookInsertionCommand = CreateCommand(bookSql, null, titleParam, isbnParam, yearParam, editionParam))
            {
                using (var reader = bookInsertionCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book.Id = reader.GetInt32(0);
                    }
                }
            }

            DbParameter bookIdParam = CreateParameter(DbType.Int32, "@BookId", book.Id);

            var commands = new List<DbCommand>();

            foreach (var subject in book.GetSubjects())
            {
                DbParameter subjectIdParam = CreateParameter(DbType.Int32, "@SubjectId", subject.Id);
                commands.Add(CreateCommand(subjectSql, null, bookIdParam, subjectIdParam));
            }

            foreach (var author in book.GetAuthors())
            {
                DbParameter authorIdParam = CreateParameter(DbType.Int32, "@AuthorId", author.Id);
                commands.Add(CreateCommand(authorSql, null, bookIdParam, authorIdParam));
            }

            ExecuteNonQueryTransactional(commands);
        }
        public override void Remove(Book book)
        {
            string deleteSubjectsSql = "DELETE FROM book_subject WHERE BookId = @Id";
            string deleteAuthorsSql = "DELETE FROM book_author WHERE BookId = @Id";
            string deleteBookSql = "DELETE FROM books WHERE Id = @Id";

            DbParameter bookIdParam = CreateParameter(DbType.Int32, "@Id", book.Id);

            var commands = new List<DbCommand>();
            commands.Add(CreateCommand(deleteSubjectsSql, parameters: bookIdParam));
            commands.Add(CreateCommand(deleteAuthorsSql, parameters: bookIdParam));
            commands.Add(CreateCommand(deleteBookSql, parameters: bookIdParam));

            ExecuteNonQueryTransactional(commands);
        }
        public override void Update(Book book)
        {
            string deleteSubjectsSql = "DELETE FROM book_subject WHERE BookId = @Id";
            string deleteAuthorsSql = "DELETE FROM book_author WHERE BookId = @Id";
            string bookSubjectInsertSql = "INSERT INTO book_subject (BookId, SubjectId) VALUES (@Id, @SubjectId)";
            string bookAuthorInsertSql = "INSERT INTO book_author (BookId, AuthorId) VALUES (@Id, @AuthorId)";
            string updateBookSql = "UPDATE books SET Title = @Title, Isbn = @Isbn, Year = @Year, Edition = @Edition WHERE Id = @Id";

            DbParameter bookIdParam = CreateParameter(DbType.Int32, "@Id", book.Id);
            DbParameter titleParam = CreateParameter(DbType.String, "@Title", book.Title);
            DbParameter isbnParam = CreateParameter(DbType.String, "@Isbn", book.Isbn);
            DbParameter yearParam = CreateParameter(DbType.String, "@Year", book.Year);
            DbParameter editionParam = CreateParameter(DbType.Int32, "@Edition", book.Edition);

            List<DbCommand> commands = new List<DbCommand>();

            commands.Add(CreateCommand(updateBookSql, null, bookIdParam, titleParam, isbnParam, yearParam, editionParam));
            commands.Add(CreateCommand(deleteSubjectsSql, null, bookIdParam));
            commands.Add(CreateCommand(deleteAuthorsSql, null, bookIdParam));

            foreach (var subject in book.GetSubjects())
            {
                DbParameter subjectIdParam = CreateParameter(DbType.Int32, "@SubjectId", subject.Id);
                commands.Add(CreateCommand(bookSubjectInsertSql, null, bookIdParam, subjectIdParam));
            }

            foreach (var author in book.GetAuthors())
            {
                DbParameter authorIdParam = CreateParameter(DbType.Int32, "@AuthorId", author.Id);
                commands.Add(CreateCommand(bookAuthorInsertSql, null, bookIdParam, authorIdParam));
            }

            ExecuteNonQueryTransactional(commands);

        }
    }
}