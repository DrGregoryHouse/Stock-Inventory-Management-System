using System.Data.SqlClient;
using StockInventorySystem.Models;

namespace StockInventorySystem.Gateway
{
    public class InsertGateway:Gateway
    {

        public bool ItemDoesExists(string itemName)
        {
            Query = "SELECT * FROM Item WHERE item='"+itemName+"' AND Status='true'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool doesExists = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return doesExists;
        }

        public int SaveItem(Item aItem)
        {
            Query = "INSERT INTO Item VALUES('" + aItem.ItemName + "','true')";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffeted = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffeted;
        }

        public bool SupplierDoesExists(string supplierName)
        {
            Query = "SELECT * FROM Supplier WHERE SupplierName='" + supplierName + "' AND Status='true'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool doesExists = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return doesExists;
        }

        public int SaveSupplier(Supplier aSupplier)
        {
            Query = "INSERT INTO Supplier VALUES('" + aSupplier.SupplierName + "','" + aSupplier.ContactPerson + "','" + aSupplier.MobileNo + "','" + aSupplier.Address + "','" + aSupplier.Email+ "','true')";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffeted = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffeted;
        }
    }
}