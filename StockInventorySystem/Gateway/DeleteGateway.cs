using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StockInventorySystem.Models;

namespace StockInventorySystem.Gateway
{
    public class DeleteGateway:Gateway
    {
        public int DeleteItemById(Item aItem)
        {
            Query = "UPDATE Item SET Status='delete' WHERE Id='" + aItem.ItemName + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int DeleteSupplierById(Supplier aSupplier)
        {
            Query = "UPDATE Supplier SET Status='delete' WHERE Id='" + aSupplier.SupplierName + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}