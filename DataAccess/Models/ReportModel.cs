using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ReportModel
    {
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        [DisplayName("Product Description")]
        public string? ProductDescription { get; set; }

        [DisplayName("Unit Price")]
        public double? ProductUnitPrice { get; set; }

        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Category Name")]
        public string? CategoryName { get; set; }

        [DisplayName("Shop Name")]
        public string? ShopName { get; set; }

        #region Filtreleme
        public int? CategoryId { get; set; }
        public int? ShopId { get; set; }
        #endregion

        [DisplayName("Expiration Date")]
        public string? ExpirationDateDisplay { get; set; }
    }
}
