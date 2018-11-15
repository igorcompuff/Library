using System.Collections.Generic;

namespace Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Description { get; }

        public Subject(string description)
        {
            Description = description;
        }
        public override List<string> Validate() => new List<string>();

        public override string ToString()
        {
            return Description;
        }
    }
}
