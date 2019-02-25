using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StockInventorySystem.Models;

namespace StockInventorySystem.Gateway
{
    public class PurchaseGateway:Gateway
    {
        public List<Supplier> GetAllSupplier()
        {
            Query = "SELECT * FROM Supplier WHERE Status='true'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Supplier> aSuppliers = new List<Supplier>();
            while (Reader.Read())
            {
                aSuppliers.Add(new Supplier()
                {
                    Id = (int)Reader["Id"],
                    SupplierName = Reader["SupplierName"].ToString(),
                    ContactPerson = Reader["ContactPerson"].ToString(),
                    Address = Reader["Address"].ToString(),
                    MobileNo = Reader["Mobile"].ToString(),
                    Email = Reader["Email"].ToString()
                });
            }
            Reader.Close();
            Connection.Close();
            return aSuppliers;
        }

        public int Save(Purchase purchase)
        {
            Query = "INSERT INTO Purchase Values('" + purchase.InvoiceNo + "','" + purchase.BillDate + "','" +
                    purchase.SupplierId + "','" + purchase.ReceiverDate + "','" + purchase.CarringCharge + "','"+purchase.TotalAmount+"','true')";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();


            Query = "SELECT * FROM Purchase where InvoiceNo='" + purchase.InvoiceNo + "'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                purchase.Id = (int) Reader["Id"];
            }
            Reader.Close();
            Connection.Close();

            foreach (PurchaseItem t in purchase.PurchaseItems)
            {
                Query = "INSERT INTO PurchaseItem Values('" + t.ItemName + "','" +
                        t.Qty + "','" + t.Price + "','" +
                        purchase.Id + "','"+t.Amount+"','"+t.Qty+"','true')";
                Command=new SqlCommand(Query,Connection);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
            }
            Connection.Close();
            return rowAffected;
        }

        public List<Item> GetAllItems()
        {
            Query = "SELECT * FROM Item WHERE Status='true'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Item> aItems=new List<Item>();
            while (Reader.Read())
            {
                aItems.Add(new Item()
                {
                    Id = (int)Reader["Id"],
                    ItemName = Reader["Item"].ToString()
                });
            }
            Reader.Close();
            Connection.Close();
            return aItems;

        }

        public string GetInvoiceNo()
        {
            string invoiceNo;
            int id = 0;
            Query = "SELECT Count(ID)As invoiceNo FROM Purchase";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                id = (int) Reader["invoiceNo"];
            }
            id++;
            Reader.Close();
            Connection.Close();
            string num = id.ToString();

            int diff = 3 - num.Length;
            for (int i = 1; i <= diff; i++)
                num = "0" + num;
            invoiceNo = DateTime.Now.Year+"" +DateTime.Now.Day+""+ num;
            Purchase aPurchase=new Purchase();
            return aPurchase.InvoiceNo = invoiceNo;
            
        }

        public List<Purchase> GetItemsByInvoiceNo(string nestId)
        {
            List<Purchase> aPurchases = new List<Purchase>();
            Query = "SELECT * FROM PurchaseView WHERE InvoiceNo='" + nestId + "' AND Status='true' OR Status='false'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
           Reader = Command.ExecuteReader();
            
            if (Reader.Read())
            {
                Purchase ap=new Purchase();
                ap.Id = (int) Reader["Id"];
                ap.BillDate = (DateTime) Reader["BillDate"];
                ap.SupplierName = Reader["SupplierName"].ToString();
                ap.ReceiverDate = (DateTime) Reader["ReceiverDate"];
                ap.CarringCharge = (decimal) Reader["CarryCharge"];

                decimal totalAmount = (decimal)Reader["TotalAmount"];
                
                ap.TotalAmount = (float)totalAmount;
                aPurchases.Add(ap);
            }
            Reader.Close();
            Connection.Close();
            Query = "SELECT * FROM PurchaseItem WHERE InvoiceId='" + aPurchases[0].Id + "' AND Status='true' OR Status='false'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                PurchaseItem aPurchaseItem=new PurchaseItem();
                aPurchaseItem.ItemName = Reader["ItemName"].ToString();
                aPurchaseItem.Qty = (int) Reader["Qty"];
              
                decimal price= (decimal)Reader["Price"];
                aPurchaseItem.Price = (float) price;

                decimal amount = (decimal)Reader["Amount"];
                aPurchaseItem.Amount = (float)amount;
                aPurchases[0].PurchaseItems.Add(aPurchaseItem);
            }
            Reader.Close();
            Connection.Close();
            return aPurchases;

        }

        public List<Purchase> GetAllInvoiceNo()
        {
            Query = "SELECT * FROM Purchase WHERE status='true' OR status='false'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Purchase> aPurchases=new List<Purchase>();
            while (Reader.Read())
            {
                aPurchases.Add(new Purchase()
                {
                    Id = (int)Reader["Id"],
                    InvoiceNo = Reader["InvoiceNo"].ToString()
                });
            }
            Reader.Close();
            Connection.Close();
            return aPurchases;
        }

        public bool DoesExists(string invoiceNo)
        {
            Query = "SELECT * FROM Purchase WHERE InvoiceNo='" + invoiceNo + "'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool doesExists = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return doesExists;
        }

        public int DeleteInvoiceById(int id)
        {
            Query = "UPDATE PurchaseItem SET status='delete' WHERE invoiceid='" + id + "'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowAffected > 0)
            {
                Query = "UPDATE Purchase SET status='delete' WHERE Id='" + id + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
            }
            return rowAffected;
        }
    }
}