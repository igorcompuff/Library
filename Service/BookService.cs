using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class BookService : BaseService<Book>
    {
        public BookService(IRepository<Book> repository, IView view) : base(repository, view)
        {  
        }

        public override void Add(Book book)
        {
            List<string> errors = book.Validate();
            if (errors.Count == 0)
            {
                _repository.Add(book);
            }
            else
            {
                _view.ShowErrors(errors);
            }
        }
    }
}
