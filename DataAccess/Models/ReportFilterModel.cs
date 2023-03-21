using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ReportFilterModel
    {
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        [DisplayName("Shops")]
        public List<int>? ShopIds { get; set; }

        [DisplayName("Expiration")]
        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}
