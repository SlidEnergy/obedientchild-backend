using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ObedientChild.Domain
{
    public class Child
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowNull]
        public byte[] Avatar { get; set; }

        public Child()
        {

        }

        public Child(string name)
        {
            Name = name;
        }
    }
}
