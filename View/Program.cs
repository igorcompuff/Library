using Domain.Entities;
using Persistence;
using Service;
using System;
using System.Collections.Generic;
using Persistence.BD;

namespace View
{
    public class Program : IView
    {
        BookService _service;

        public Program()
        {
            _service = new BookService(new BookBdRepository(), this);
        }

        public static void Main(string[] args)
        {
            var p = new Program();

            //p.ShowMenu();
            BookBdRepository rep = new BookBdRepository();
            Book book = rep.GetById(1);

            Console.WriteLine($"Título: {book.Title}\nIsbn: {book.Isbn}\nEdição: {book.Edition}\nYear: {book.Year}");

            foreach (var author in book.GetAuthors())
            {
                Console.WriteLine(author);
            }
            Console.ReadKey();
        }

        /*
        public void ShowMenu()
        {
            string option = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Menu da Biblioteca\n");
                Console.WriteLine("1  - Cadastrar Livro");
                Console.WriteLine("2  - Buscar livro pelo título");
                Console.WriteLine("3  - Excluir livro");
                Console.WriteLine("4  - Alterar livro");
                Console.WriteLine("10 - Sair");

                Console.Write("\nEntre com a opção desejada: ");
                option = Console.ReadLine();

            } while (ExecuteOption(option));
        }

       
        public bool ExecuteOption(string option)
        {
            Console.Clear();
            switch (option)
            {
                case "1":
                    _service.Add(GetBook());
                    break;
                case "2":
                    Console.WriteLine("Informe o título do Livro");
                    string title = Console.ReadLine();
                    Book book = _service.Get(title);
                    ShowBook(book);
                    break;
                case "3":
                    Console.WriteLine("Informe o título do Livro");
                    title = Console.ReadLine();
                    book = _service.Get(title);

                    if (book != null)
                    {
                        _service.Remove(book);
                    }
                    else
                    {
                        Console.WriteLine("Livro Não Encontrado!");
                    }
                    break;
                case "4":
                    Console.WriteLine("Informe o título do Livro");
                    title = Console.ReadLine();
                    book = _service.Get(title);
                    Book newBook = GetBookUpdate(book);
                    _service.Update(newBook);
                    break;
                case "10":
                    return false;
            }

            return true;
        }

        public void ShowBook(Book book)
        {
            Console.Clear();
            Console.WriteLine($"Título: {book.Title}");
            Console.WriteLine($"ISBN: {book.Isbn}");
            Console.WriteLine($"Edição: {book.Edition}");
            Console.WriteLine($"Ano: {book.Year}");

            Console.WriteLine("Autores:");
            foreach(var author in book.GetAuthors())
            {
                Console.WriteLine($"- {author}");
            }

            Console.WriteLine("Assuntos:");
            foreach (var subject in book.GetSubjects())
            {
                Console.WriteLine($"- {subject}");
            }

            Console.ReadKey();

        }

        public Book GetBookUpdate(Book oldBook)
        {
            Console.Clear();
            Console.WriteLine($"Entre com o título do Livro[{oldBook.Title}]");
            string title = Console.ReadLine();
            title = title == "" ? oldBook.Title : title;

            Console.WriteLine($"Entre com a edição do livro[{oldBook.Edition}]");
            uint edition;

            if (!uint.TryParse(Console.ReadLine(), out edition))
            {
                edition = oldBook.Edition;
            }

            Console.WriteLine($"Entre com o ano do livro[{oldBook.Year}]");
            string year = Console.ReadLine();
            year = year == "" ? oldBook.Year : year;

            Book book = new Book(title, oldBook.Isbn, edition, year);

            
            foreach (var author in oldBook.GetAuthors())
            {
                Console.WriteLine($"Gostaria de Alterar o Autor {author}?(s/n)");
                string update = Console.ReadLine();

                if (update != "s")
                {
                    book.AddAuthor(author);
                }
                else
                {
                    book.RemoveAuthor(author);
                    Console.WriteLine("Digite o nome do autor");
                    book.AddAuthor(Console.ReadLine());
                }
            }

            foreach (var subject in oldBook.GetSubjects())
            {
                Console.WriteLine($"Gostaria de Alterar o Assunto {subject}?(s/n)");
                string update = Console.ReadLine();

                if (update != "s")
                {
                    book.AddSubject(subject);
                }
                else
                {
                    book.RemoveSubject(subject);
                    Console.WriteLine("Digite o novo assunto");
                    book.AddSubject(Console.ReadLine());
                }
            }

            string option;

            Console.WriteLine("Gostaria de adicionar novos autores?");
            if (Console.ReadLine() == "s")
            {
                do
                {
                    Console.WriteLine("Digite o nome do autor");
                    book.AddAuthor(Console.ReadLine());

                    Console.WriteLine("Deseja adicionar outro autor? (s/n)");
                    option = Console.ReadLine();
                } while (option.ToLower() != "n");
            }

            Console.WriteLine("Gostaria de adicionar novos assuntos?");
            if (Console.ReadLine() == "s")
            {
                do
                {
                    Console.WriteLine("Digite o assunto");
                    book.AddSubject(Console.ReadLine());

                    Console.WriteLine("Deseja adicionar outro assunto? (s/n)");
                    option = Console.ReadLine();
                } while (option.ToLower() != "n");
            }

            return book;
        }

        public Book GetBook()
        {
            Console.Clear();
            Console.WriteLine($"Entre com o título do Livro");
            string title = Console.ReadLine();

            Console.WriteLine($"Entre com o ISBN do Livro");
            string isbn = Console.ReadLine();

            Console.WriteLine($"Entre com a edição do livro");
            uint edition = uint.Parse(Console.ReadLine());
            
            Console.WriteLine($"Entre com o ano do livro");
            string year = Console.ReadLine();

            Book book = new Book(title, isbn, edition, year);

            string option;
            do
            {
                Console.WriteLine("Digite o nome do autor");
                book.AddAuthor(Console.ReadLine());

                Console.WriteLine("Deseja adicionar outro autor? (s/n)");
                option = Console.ReadLine();
            } while (option.ToLower() != "n");

            do
            {
                Console.WriteLine("Digite o assunto");
                book.AddSubject(Console.ReadLine());

                Console.WriteLine("Deseja adicionar outro assunto? (s/n)");
                option = Console.ReadLine();
            } while (option.ToLower() != "n");

            return book;
        }
        */
        public void ShowErrors(List<string> errors)
        {
            
        }
    }
}
