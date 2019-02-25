using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }
         
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }

    }
}