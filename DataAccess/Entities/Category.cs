using AppCore8137.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public partial class Category : Record
    {
        [Required]
        [StringLength(100)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }

        [DisplayName("Category Description")]
        [StringLength(500, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }

    public partial class Category
    {
        [NotMapped]
        [DisplayName("Product Count")]
        public int? ProductCountDisplay { get; set; }
    }
}
