using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockInventorySystem.Gateway;
using StockInventorySystem.Models;

namespace StockInventorySystem.Manager
{
    public class DeleteManager
    {
        DeleteGateway aDeleteGateway=new DeleteGateway();
        public string DeleteItemById(Item aItem)
        {
            int rowAffected = aDeleteGateway.DeleteItemById(aItem);
            if (rowAffected>0)
            {
                return "Yes";
            }
            return "No";
        }

        public string DeleteSupplierById(Supplier aSupplier)
        {
            int rowAffected = aDeleteGateway.DeleteSupplierById(aSupplier);
            if (rowAffected > 0)
            {
                return "Yes";
            }
            return "No";
        }
    }
}