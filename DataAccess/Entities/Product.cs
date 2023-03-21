using AppCore8137.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public partial class Product : Record
    {
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

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public List<ProductShop>? ProductShops { get; set; }

        [JsonIgnore]
        [Column(TypeName = "image")]
        public byte[]? Image { get; set; }

        [JsonIgnore]
        [StringLength(5)]
        public string? ImageExtension { get; set; } // .jpg, bmp, png
    }

    public partial class Product
    {
        [NotMapped]
        [DisplayName("Unit Price")]
        public string? UnitPriceDisplay { get; set; }

        [NotMapped]
        [DisplayName("Expiration Date")]
        [JsonIgnore]
        public string? ExpirationDateDisplay => ExpirationDate == null ? "" : ExpirationDate.Value.ToString("MM/dd/yyyy");

        [NotMapped]
        [DisplayName("Category")]
        public string? CategoryNameDisplay { get; set; }

        [NotMapped]
        [DisplayName("Shops")]
        //[Required]
        public List<int>? ShopIds { get; set; } // listbox üzerinden idleri alabilmek için

        [NotMapped]
        [DisplayName("Shops")]
        public string? ShopNamesDisplay { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public string? ImageTagSrcDisplay { get; set; }
    }
}
