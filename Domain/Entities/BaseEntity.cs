using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        protected BaseEntity()
        {
            Id = -1;
        }
        public override bool Equals(object obj) => obj is BaseEntity entity && entity.Id.Equals(Id);

        public abstract List<string> Validate();
    }
}
