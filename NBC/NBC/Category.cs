using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBC
{
    public class Category : IEquatable<Category>
    {
        public Category(string name, string file)
        {
            this.File = file;
            this.Name = name;
        }

        public Dictionary<string,int> words { get; } = new Dictionary<string, int>();

        public string Name { get; set; }

        public string File { get; set; }

        public int PositiveHits { get; set; } = 0;

        public int NegativeHits { get; set; } = 0;

        public bool Equals(Category other)
        {
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
