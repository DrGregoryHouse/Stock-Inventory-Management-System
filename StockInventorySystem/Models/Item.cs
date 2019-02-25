using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Item Name: ")]
        public string ItemName { get; set; }
    }
}