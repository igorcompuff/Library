using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public object Id { get; set; }

        protected BaseEntity()
        {
            Id = -1;
        }
        public override bool Equals(object obj) => obj is BaseEntity entity && entity.Id.Equals(Id);

        public abstract List<string> Validate();
    }
}
