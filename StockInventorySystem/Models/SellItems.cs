using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class SellItems
    {
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public int SupplierId { get; set; }

    }
}