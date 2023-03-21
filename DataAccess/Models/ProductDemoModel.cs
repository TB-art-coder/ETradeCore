using AppCore8137.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DataAccess.Models
{
    public class ProductDemoModel : Record
    {
        // entity'den gelenler
        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Product Name")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string? Name { get; set; }

        [DisplayName("Product Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")]
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Unit Price")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be between {1} and {2}!")]
        [Required(ErrorMessage = "{0} is required!")]
        public double? UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }


        
        
        [DisplayName("Unit Price")]
        public string? UnitPriceDisplay { get; set; }

        [DisplayName("Expiration Date")]
        public string? ExpirationDateDisplay { get; set; }

        [DisplayName("Category")]
        public string? CategoryDisplay { get; set; }
    }
}
