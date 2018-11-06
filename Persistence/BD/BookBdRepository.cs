using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD
{
    public class BookBdRepository : BaseBdRepository<Book>, IBookRepository
    {
        public override Book GetById(object id)
        {
            Book book = null;

            using (DbCommand query = _factory.CreateCommand())
            {
                if (query != null)
                {
                    query.Connection = _connection;
                    query.CommandText = "Select * from Books Where Id = @Id";

                    DbParameter parameter = _factory.CreateParameter();
                    parameter.DbType = DbType.Int32;
                    parameter.ParameterName = "@Id";
                    parameter.Value = id;

                    query.Parameters.Add(parameter);

                    using (var reader = query.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int bookId = (int) reader["Id"];
                            string title = reader["Title"].ToString();
                            string isbn = reader["Isbn"].ToString();
                            int edition = (int)reader["Edition"];
                            string year = reader["Year"].ToString();

                            book = new Book(title, isbn, edition, year){Id = bookId };
                        }
                    }
                }
            }

            if (book != null)
            {
                FillAuthors(book);
            }

            return book;
        }

        public override void Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Book entity)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Book GetBookByIsbn(string isbn)
        {
            throw new NotImplementedException();
        }

        private void FillAuthors(Book book)
        {
            using (var command = _factory.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "Select Authors.Name from Authors Inner Join Book_Author on Authors.Id = Book_Author.AuthorId and Book_Author.BookId = @Id";

                var parameter = _factory.CreateParameter();
                parameter.DbType = DbType.Int32;
                parameter.ParameterName = "@Id";
                parameter.Value = book.Id;

                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.AddAuthor(reader.GetString(0));
                    }
                }
            }
        }
    }
}
