using System.Collections.Generic;


namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; }

        public Author(string name)
        {
            Name = name;
        }

        public Author()
        {
            
        }
        public override List<string> Validate() => new List<string>();
        public override string ToString()
        {
            return Name;
        }
    }
}
