using System.Collections.Generic;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        private readonly List<Author> _authors = new List<Author>();
        private readonly List<Subject> _subjects = new List<Subject>();
        public string Title { get; }
        public string Isbn { get; }
        public int Edition { get; }
        public string Year { get; }

        public Book(string title, string isbn, int edition, string year)
        {
            Title = title;
            Isbn = isbn;
            Edition = edition;
            Year = year;
        }

        public void AddAuthor(Author author) => _authors.Add(author);
        public void AddAuthors(IEnumerable<Author> authors) => _authors.AddRange(authors);
        public void RemoveAuthor(Author author) => _authors.Remove(author);
        public void AddSubject(Subject subject) => _subjects.Add(subject);
        public void AddSubjects(IEnumerable<Subject> subjects) => _subjects.AddRange(subjects);
        public void RemoveSubject(Subject subject) => _subjects.Remove(subject);
        public IReadOnlyCollection<Author> GetAuthors() => _authors.AsReadOnly();
        public IReadOnlyCollection<Subject> GetSubjects() => _subjects.AsReadOnly();
        public override List<string> Validate() => new List<string>();

    }
}