
#nullable disable

using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ETradeCoreBilgeAdam.Models
{
    public class HomeIndexViewModel
    {
        public List<ReportModel> Report { get; set; }

        public SelectList Categories { get; set; }

        public ReportFilterModel Filter { get; set; }

        public MultiSelectList Shops { get; set; }
    }
}
