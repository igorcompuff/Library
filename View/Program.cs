using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using Persistence.BD;
using Persistence.BD.Repositories;

namespace View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var prog = new Program();

            //prog.TestGetById();
            prog.GetAll();
            //prog.TestAdd();
            //prog.TestUpdate();
            //prog.TestRemove();
        }

        public void TestGetById()
        {
            using (var repository = new BookBdRepository())
            {
                Book book = repository.GetById(1);
                ShowBook(book);
                Console.ReadKey();
            }
        }

        public void GetAll()
        {
            using (var rep = new BookBdRepository())
            {
                foreach (var book in rep.GetAll())
                {
                    ShowBook(book);
                    Console.WriteLine("___________");
                }
            }

            Console.ReadKey();
        }

        public void TestAdd()
        {
            var book = new Book("Saphiens", "2121", 1, "2017");
            book.AddAuthor(new Author("Gabriela") { Id = 3 });
            book.AddSubject(new Subject("Magia") { Id = 3 });

            using (var rep = new BookBdRepository())
            {
                rep.Add(book);

                foreach (var b in rep.GetAll())
                {
                    ShowBook(b);
                }
            }

            Console.ReadKey();
        }

        public void TestUpdate()
        {
            var book = new Book("Saphiens 2", "2121", 1, "2017") { Id = 9 };
            book.AddAuthor(new Author("Gabriela") { Id = 3 });
            book.AddSubject(new Subject("C#") { Id = 4 });

            using (var rep = new BookBdRepository())
            {
                rep.Update(book);

                foreach (var b in rep.GetAll())
                {
                    ShowBook(b);
                }
            }

            Console.ReadKey();
        }

        public void TestRemove()
        {
            var book = new Book("Saphiens 2", "2121", 1, "2017") { Id = 9 };
            book.AddAuthor(new Author("Gabriela") { Id = 3 });
            book.AddSubject(new Subject("C#") { Id = 4 });

            using (var rep = new BookBdRepository())
            {
                rep.Remove(book);
            }
        }

        public static void ShowBook(Book book)
        {
            Console.WriteLine($"Título: {book.Title}");
            Console.WriteLine($"ISBN: {book.Isbn}");
            Console.WriteLine($"Edição: {book.Edition}");
            Console.WriteLine($"Ano: {book.Year}");

            Console.WriteLine("Autores:");
            foreach (var author in book.GetAuthors())
            {
                Console.WriteLine($"- {author}");
            }

            Console.WriteLine("Assuntos:");
            foreach (var subject in book.GetSubjects())
            {
                Console.WriteLine($"- {subject}");
            }
        }
    }
}