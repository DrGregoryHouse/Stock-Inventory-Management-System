using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Supplier Name: ")]
        public string SupplierName { get; set; }
        [Required]
        [DisplayName("Address: ")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Mobile No: ")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Type Valid Email No")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Input a valid email address")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact Person: ")]
        public string ContactPerson { get; set; }

    }
}