using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public class AuthorBdRepository: BaseBdRepository<Author>, IAuthorRepository
    {
        public AuthorBdRepository(UnityOfWork unityOfWork) : base(unityOfWork)
        {
        }

        public override void Add(Author entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Author entity)
        {
            throw new NotImplementedException();
        }

        public override Author GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAuthorsOfABook(Book book)
        {
            string sql = "Select * from Authors Inner Join Book_Author on Authors.Id = Book_Author.AuthorId and Book_Author.BookId = @Id"; ;
            var authors = new List<Author>();
            DbParameter parameter = _unityOfWork.CreateParameter(DbType.Int32, "@Id", book.Id);

            using (var command = _unityOfWork.CreateCommand(sql, parameter))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = reader["Name"].ToString();

                        authors.Add(new Author(name) { Id = id });
                    }
                }
            }

            return authors;
        }
    }
}
