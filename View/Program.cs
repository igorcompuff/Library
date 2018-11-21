using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Service;

namespace View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var prog = new Program();

            prog.TestRemove();


        }

        public void TestGetById()
        {
            BookService bookService = new BookService();
            ShowBook(bookService.GetBookById(1));
            Console.ReadKey();
        }

        public void GetAll()
        {
            BookService bookService = new BookService();

            foreach (var book in bookService.GetAllBooks())
            {
                ShowBook(book);
                Console.WriteLine("___________");
            }

            Console.ReadKey();
        }

        private void TestAdd(Book book)
        {
            BookService bookService = new BookService();
            IEnumerable<string> errors = bookService.AddBook(book);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Livro adicionado com sucesso");
            }

            Console.ReadKey();
        }

        public void TestAdd()
        {
            var book = new Book("Saphiens", "2121", 1, "2017");
            book.AddAuthor(new Author("Gabriela") { Id = 3 });
            book.AddSubject(new Subject("Magia") { Id = 3 });

            TestAdd(book);
        }

        public void TestUpdate()
        {
            var book = new Book("Saphiens 3", "2121", 1, "2017") { Id = 13 };
            book.AddAuthor(new Author("Gabriela") { Id = 3 });
            book.AddSubject(new Subject("C#") { Id = 4 });

            TestAdd(book);
        }

        public void TestRemove()
        {
            var book = new Book("Saphiens 3", "2121", 1, "2017") { Id = 13 };
            BookService bookService = new BookService();
            bookService.RemoveBook(book);

            Console.WriteLine("Livro removido.");
            Console.ReadKey();
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