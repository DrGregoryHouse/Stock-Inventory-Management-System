using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StockInventorySystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        [DisplayName("Invoice No")]
        public string InvoiceNo { get; set; }
        [DisplayName("Supplier Name")]
        public int SupplierId { get; set; }
        [DisplayName("Bill Date")]
        public DateTime BillDate{ get; set; }
        [DisplayName("Receive Date")]
        public DateTime ReceiverDate{ get; set; }
        [DisplayName("Carring Charge")]
        public decimal CarringCharge { get; set; }
        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }
        [DisplayName("Total Amount")]
        public float TotalAmount { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        public Purchase()
        {
            PurchaseItems = new List<PurchaseItem>();
        }

    }
}
