using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public class SubjectBdRepository: BaseBdRepository<Subject>, ISubjectRepository
    {
        public SubjectBdRepository(UnityOfWork unityOfWork) : base(unityOfWork)
        {
        }

        public override void Add(Subject entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Subject entity)
        {
            throw new NotImplementedException();
        }

        public override Subject GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Subject> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> GetSubjectsOfABook(Book book)
        {
            string sql = "Select * from Subjects Inner Join Book_Subject on Subjects.Id = Book_Subject.SubjectId and Book_Subject.BookId = @Id";
            var subjects = new List<Subject>();
            DbParameter parameter = _unityOfWork.CreateParameter(DbType.Int32, "@Id", book.Id);

            using (var command = _unityOfWork.CreateCommand(sql, parameter))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string description = reader["Subject"].ToString();

                        subjects.Add(new Subject(description) { Id = id });
                    }
                }
            }

            return subjects;
        }
    }
}
