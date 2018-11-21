using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public string Index()
        {
            return "Controlador de Livros - Index";
        }

        public ActionResult List()
        {
            List<Author> authors = new List<Author>();

            for (int i = 1; i <= 10; i++)
            {
                authors.Add(new Author($"Teste {i}"));
            }

            return View(authors);
        }

        public string Details(int id)
        {
            return HttpUtility.HtmlEncode($"Controlador de Livros - Details({id})");
        }
    }
}