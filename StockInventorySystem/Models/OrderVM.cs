using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace StockInventorySystem.Models
{
    public class OrderVM
    {
        [Required(ErrorMessage = "Order No required")]
        [DisplayName("Order No")]
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}