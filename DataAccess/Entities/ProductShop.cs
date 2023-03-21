using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class ProductShop
    {
        [Key]
        [Column(Order = 0)]
        public int? ProductId { get; set; }

        public Product? Product { get; set; }

        [Key]
        [Column(Order = 1)]
        public int? ShopId { get; set; }

        public Shop? Shop { get; set; }
    }
}
