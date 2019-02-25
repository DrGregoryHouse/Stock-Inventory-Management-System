using System.Collections.Generic;
using StockInventorySystem.Gateway;
using StockInventorySystem.Models;

namespace StockInventorySystem.Manager
{
    public class PurchaseManager
    {
        PurchaseGateway aPurchaseGateway=new PurchaseGateway();

        public List<Supplier> GetAllSupplier()
        {
            return aPurchaseGateway.GetAllSupplier();
            
        }

        public string Save(Purchase purchase)
        {
            bool doesExists = aPurchaseGateway.DoesExists(purchase.InvoiceNo);
            if (doesExists)
            {
                return "Exists";
            }
            int rowAffected = aPurchaseGateway.Save(purchase);
            if (rowAffected>0)
            {
                return "Yes";
            }
            return "No";

        }

        public List<Item> GetAllItems()
        {
            return aPurchaseGateway.GetAllItems();
        }

        public string GetInvoiceNo()
        {
            return aPurchaseGateway.GetInvoiceNo();
        }

        public List<Purchase> GetItemsByInvoiceNo(string nestId)
        {
            return aPurchaseGateway.GetItemsByInvoiceNo(nestId);
        }

        public List<Purchase> GetAllInvoiceNo()
        {
            return aPurchaseGateway.GetAllInvoiceNo();
        }

        public string DeleteInvoiceById(int id)
        {
            int rowAffected = aPurchaseGateway.DeleteInvoiceById(id);
            if (rowAffected > 0)
            {
                return "Yes";
            }
            return "No";
        }
    }
}