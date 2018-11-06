using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookInstanceService : BaseService<BookInstance>
    {
        public BookInstanceService(IRepository<BookInstance> repository, IView view) : base(repository, view)
        {
        }

        public override void Add(BookInstance bookInstance)
        {
            List<string> errors = bookInstance.Validate();
            if (errors.Count == 0)
            {
                _repository.Add(bookInstance);
            }
            else
            {
                _view.ShowErrors(errors);
            }
        }
    }
}
