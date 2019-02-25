using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class OrderDetails
    {
        public string OrderItemsId { get; set; }
        public string OrderId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }

    }
}