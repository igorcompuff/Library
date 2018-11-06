using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BookInstance : BaseEntity
    {
        public bool Available { get; }
        public string Code { get; }

        public BookInstance(bool available)
        {
            Available = available;
            Code = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            BookInstance instance = obj as BookInstance;

            return (instance != null) && (instance.Code == Code);
        }

        public override List<string> Validate()
        {
            return new List<string>();
        }
    }
}
