using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public class BookBdRepository : BaseBdRepository<Book>, IBookRepository
    {
        public BookBdRepository(UnityOfWork unityOfWork): base(unityOfWork)
        {
            
        }

        private Book ReadBook(DbDataReader reader)
        {
            int bookId = (int)reader["Id"];
            string title = reader["Title"].ToString();
            string isbn = reader["Isbn"].ToString();
            int edition = (int)reader["Edition"];
            string year = reader["Year"].ToString();

            return new Book(title, isbn, edition, year) { Id = bookId };
        }
        private void UpdateBook(Book book)
        {
            string deleteSubjectsSql = "DELETE FROM book_subject WHERE BookId = @Id";
            string deleteAuthorsSql = "DELETE FROM book_author WHERE BookId = @Id";
            string bookSubjectInsertSql = "INSERT INTO book_subject (BookId, SubjectId) VALUES (@Id, @SubjectId)";
            string bookAuthorInsertSql = "INSERT INTO book_author (BookId, AuthorId) VALUES (@Id, @AuthorId)";
            string updateBookSql = "UPDATE books SET Title = @Title, Isbn = @Isbn, Year = @Year, Edition = @Edition WHERE Id = @Id";

            DbParameter bookIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@Id", book.Id);
            DbParameter titleParam = _unityOfWork.CreateParameter(DbType.String, "@Title", book.Title);
            DbParameter isbnParam = _unityOfWork.CreateParameter(DbType.String, "@Isbn", book.Isbn);
            DbParameter yearParam = _unityOfWork.CreateParameter(DbType.String, "@Year", book.Year);
            DbParameter editionParam = _unityOfWork.CreateParameter(DbType.Int32, "@Edition", book.Edition);

            using (DbCommand deleteSubjectCmd = _unityOfWork.CreateCommand(deleteSubjectsSql, bookIdParam),
                             deleteAuthorCmd = _unityOfWork.CreateCommand(deleteAuthorsSql, bookIdParam),
                             updateBookCmd = _unityOfWork.CreateCommand(updateBookSql, bookIdParam, titleParam, isbnParam, yearParam, editionParam))
            {
                updateBookCmd.ExecuteNonQuery();
                deleteSubjectCmd.ExecuteNonQuery();
                deleteAuthorCmd.ExecuteNonQuery();
            }

            foreach (var subject in book.GetSubjects())
            {
                DbParameter subjectIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@SubjectId", subject.Id);

                using (var command = _unityOfWork.CreateCommand(bookSubjectInsertSql, bookIdParam, subjectIdParam))
                {
                    command.ExecuteNonQuery();
                }
            }

            foreach (var author in book.GetAuthors())
            {
                DbParameter authorIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@AuthorId", author.Id);

                using (var command = _unityOfWork.CreateCommand(bookAuthorInsertSql, bookIdParam, authorIdParam))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void AddNewBook(Book book)
        {
            string bookSql = "INSERT INTO books (Title, ISBN, Year, Edition) VALUES (@Title, @Isbn, @Year, @Edition); SELECT MAX(Id) FROM books";
            string subjectSql = "INSERT INTO book_subject (BookId, SubjectId) VALUES (@BookId, @SubjectId)";
            string authorSql = "INSERT INTO book_author (BookId, AuthorId) VALUES (@BookId, @AuthorId)";

            DbParameter titleParam = _unityOfWork.CreateParameter(DbType.String, "@Title", book.Title);
            DbParameter isbnParam = _unityOfWork.CreateParameter(DbType.String, "@Isbn", book.Isbn);
            DbParameter yearParam = _unityOfWork.CreateParameter(DbType.String, "@Year", book.Year);
            DbParameter editionParam = _unityOfWork.CreateParameter(DbType.Int32, "@Edition", book.Edition);


            using (var bookInsertionCommand = _unityOfWork.CreateCommand(bookSql, titleParam, isbnParam, yearParam, editionParam))
            {
                book.Id = (int)bookInsertionCommand.ExecuteScalar();
            }

            DbParameter bookIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@BookId", book.Id);

            foreach (var subject in book.GetSubjects())
            {
                DbParameter subjectIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@SubjectId", subject.Id);
                _unityOfWork.CreateCommand(subjectSql, bookIdParam, subjectIdParam).ExecuteNonQuery();
            }

            foreach (var author in book.GetAuthors())
            {
                DbParameter authorIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@AuthorId", author.Id);
                _unityOfWork.CreateCommand(authorSql, bookIdParam, authorIdParam).ExecuteNonQuery();
            }
        }
        public override Book GetById(object id)
        {
            string sql = "Select * from Books Where Id = @Id";
            var parameter = _unityOfWork.CreateParameter(DbType.Int32, "@Id", id);
            Book book = null;
            using (var command = _unityOfWork.CreateCommand(sql, parameter))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = ReadBook(reader);
                    }
                }
            }

            return book;
        }
        public override IEnumerable<Book> GetAll()
        {
            string sql = "Select * from Books";
            
            var books = new List<Book>();
            using (var command = _unityOfWork.CreateCommand(sql))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(ReadBook(reader));
                    }
                }
            }

            return books;
        }
        public override void Add(Book book)
        {
            if ((int)book.Id < 0)
            {
                AddNewBook(book);
            }
            else
            {
                UpdateBook(book);
            }
        }
        public override void Remove(Book book)
        {
            string deleteSubjectsSql = "DELETE FROM book_subject WHERE BookId = @Id";
            string deleteAuthorsSql = "DELETE FROM book_author WHERE BookId = @Id";
            string deleteBookSql = "DELETE FROM books WHERE Id = @Id";

            DbParameter bookIdParam = _unityOfWork.CreateParameter(DbType.Int32, "@Id", book.Id);

            using (DbCommand deleteSubjectCmd = _unityOfWork.CreateCommand(deleteSubjectsSql, bookIdParam),
                             deleteAuthorCmd = _unityOfWork.CreateCommand(deleteAuthorsSql, bookIdParam),
                             deleteBookCmd = _unityOfWork.CreateCommand(deleteBookSql, bookIdParam))
            {
                deleteSubjectCmd.ExecuteNonQuery();
                deleteAuthorCmd.ExecuteNonQuery();
                deleteBookCmd.ExecuteNonQuery();
            }
        }
    }
}