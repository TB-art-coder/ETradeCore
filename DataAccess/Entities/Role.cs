using AppCore8137.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Role : Record
    {
        [Required]
        public string? Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
