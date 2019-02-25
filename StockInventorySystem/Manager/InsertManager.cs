using StockInventorySystem.Gateway;
using StockInventorySystem.Models;

namespace StockInventorySystem.Manager
{
    public class InsertManager
    {
        InsertGateway aInsertGateway = new InsertGateway();

        public string SaveItem(Item aItem)
        {
            bool doesExists = DoesExists(aItem.ItemName);
            if (!doesExists)
            {
                int rowAffected = aInsertGateway.SaveItem(aItem);
                if (rowAffected > 0)
                {
                    return "Yes";
                }
                return "No";
            }
            return "Exists";
        }

        public bool DoesExists(string itemName)
        {
            bool doesExists = aInsertGateway.ItemDoesExists(itemName);
            return doesExists;
        }

        public string SaveSupplier(Supplier aSupplier)
        {
            bool doesExists = SupplierDoesExists(aSupplier.SupplierName);
            if (!doesExists)
            {
                int rowAffected = aInsertGateway.SaveSupplier(aSupplier);
                if (rowAffected > 0)
                {
                    return "Yes";

                }
                return "No";
            }
            return "Exists";
        }
        public bool SupplierDoesExists(string supplierName)
        {
            bool doesExists = aInsertGateway.SupplierDoesExists(supplierName);
            return doesExists;
        }
    }
}