using System.Collections.Generic;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        private readonly List<string> _authors = new List<string>();
        private readonly List<string> _subjects = new List<string>();
        private readonly List<BookInstance> _instances = new List<BookInstance>();
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

        public void AddAuthor(string author)
        {
            _authors.Add(author);
        }

        public void RemoveAuthor(string author)
        {
            _authors.Remove(author);
        }

        public void AddSubject(string subject)
        {
            _subjects.Add(subject);
        }

        public void RemoveSubject(string subject)
        {
            _subjects.Remove(subject);
        }

        public void AddBookInstance(BookInstance instance)
        {
            _instances.Add(instance);
        }

        public void RemoveBookInstance(BookInstance instance)
        {
            _instances.Remove(instance);
        }

        public IReadOnlyCollection<string> GetAuthors()
        {
            return _authors.AsReadOnly();
        }

        public IReadOnlyCollection<string> GetSubjects()
        {
            return _subjects.AsReadOnly();
        }

        public IReadOnlyCollection<BookInstance> GetInstances()
        {
            return _instances.AsReadOnly();
        }

        public override List<string> Validate()
        {
            return new List<string>();
        }

    }
}
