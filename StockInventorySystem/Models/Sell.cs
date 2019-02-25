using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class Sell
    {
        public int Id { get; set; }
        [DisplayName("Bill No: ")]
        public string BillNo { get; set; }
        [Required]
        [DisplayName("Name: ")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Mobile No: ")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Type Valid Email No")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Input a valid email address")]
        public string Email { get; set; }
        [DisplayName("Total Amount: ")]
        public float TotalAmount { get; set; }
        public List<SellItems> SellItemses { get; set; }

        public Sell()
        {
            SellItemses = new List<SellItems>();
        }
    }
}