using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StockInventorySystem.Models;

namespace StockInventorySystem.Gateway
{
    public class SellGateway:Gateway
    {
        public string GetBillNo()
        {
            string billNo = null;
            int id = 0;
            Query = "SELECT Count(ID)As billNo FROM Sell";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                id = (int)Reader["BillNo"];
            }
            id++;
            Reader.Close();
            Connection.Close();
            string num = id.ToString();

            int diff = 3 - num.Length;
            for (int i = 1; i <= diff; i++)
                num = "0" + num;
            billNo = DateTime.Now.Year + "" + DateTime.Now.Day + "" + num;
            Purchase aPurchase = new Purchase();
            return aPurchase.InvoiceNo = billNo;
        }

        public string CheckItems(SellItems aSell)
        {
            Query= "SELECT * FROM ItemCheckView Where SupplierId='"+aSell.SupplierId+"' AND ItemName=@itemName AND Status='true'";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("itemName", SqlDbType.VarChar);
            Command.Parameters["itemName"].Value = aSell.ItemName;
            string msg=null;
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                var qty = (int) Reader["RemainingQty"];
                if (aSell.Qty>=qty)
                {
                    msg = "no";
                }
                else
                {
                    msg = "yes";
                }

            }
            Reader.Close();
            Connection.Close();
            return msg;
        }

        public bool DoesExists(string billNo)
        {
            Query = "SELECT * FROM Sell WHERE BillNo='" + billNo + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool doesExists = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return doesExists;
        }

        public int Save(Sell aSell)
        {
            Query = "INSERT INTO Sell Values('" + aSell.BillNo + "','" + aSell.Name + "','" +
                   aSell.Date + "','" + aSell.MobileNo + "','" + aSell.Email + "','" + aSell.TotalAmount + "','true')";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();


            Query = "SELECT * FROM Sell where BillNo='" + aSell.BillNo + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                aSell.Id = (int)Reader["Id"];
            }
            Reader.Close();
            Connection.Close();

            foreach (SellItems t in aSell.SellItemses)
            {
                Query = "INSERT INTO SellItems Values('" + t.ItemName + "','" +
                        t.SupplierId + "','" +
                        t.Qty + "','" + t.Price + "','" + t.Amount + "','" + aSell.Id + "','true')";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
            }
            Connection.Close();
            foreach (SellItems aItems in aSell.SellItemses)
            {
                int remainingQty = 0,id=0;
                Query = "SELECT * FROM ItemCheckView Where SupplierId='" + aItems.SupplierId + "' AND ItemName=@itemName AND status='true'";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Command.Parameters.Add("itemName", SqlDbType.VarChar);
                Command.Parameters["itemName"].Value = aItems.ItemName;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    remainingQty = (int) Reader["RemainingQty"];
                    id = (int) Reader["ItemId"];
                }
                Reader.Close();
                Connection.Close();
                remainingQty = remainingQty - aItems.Qty;
                if (remainingQty>0)
                {
                    Query = "UPDATE PurchaseItem SET RemainingQty='" + remainingQty + "' WHERE Id='" + id + "'";
                }
                else
                {
                    Query = "UPDATE PurchaseItem SET RemainingQty='" + remainingQty + "',status='false' WHERE Id='" + id + "'";
                }
                
                Command=new SqlCommand(Query,Connection);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();

            }
            return rowAffected;
        }

        public List<Sell> GetItemsByBillNo(string nestId)
        {
            List<Sell> aSell = new List<Sell>();
            Query = "SELECT * FROM Sell WHERE BillNo='" + nestId + "' AND status='true' OR status='false'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                Sell ap = new Sell();
                ap.Id = (int)Reader["Id"];
                ap.Date = (DateTime)Reader["Date"];
                ap.Name = Reader["Name"].ToString();
                ap.MobileNo = Reader["MobileNo"].ToString();
                ap.Email = Reader["Email"].ToString();

                decimal totalAmount = (decimal)Reader["TotalAmount"];

                ap.TotalAmount = (float)totalAmount;
                aSell.Add(ap);
            }
            Reader.Close();
            Connection.Close();
            Query = "SELECT * FROM SellItems WHERE BillNo='" + aSell[0].Id + "' AND status='true' OR status='false'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                SellItems aSellItem = new SellItems();
                aSellItem.ItemName = Reader["ItemName"].ToString();
                aSellItem.Qty = (int)Reader["Qty"];

                decimal price = (decimal)Reader["Price"];
                aSellItem.Price = (float)price;

                decimal amount = (decimal)Reader["Amount"];
                aSellItem.Amount = (float)amount;
                aSell[0].SellItemses.Add(aSellItem);
            }
            Reader.Close();
            Connection.Close();
            return aSell;
        }

        public List<Sell> GetAllBillNo()
        {
            Query = "SELECT * FROM Sell Where status='false' OR status='true'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Sell> aSells = new List<Sell>();
            while (Reader.Read())
            {
                aSells.Add(new Sell()
                {
                    Id = (int)Reader["Id"],
                    BillNo = Reader["BillNo"].ToString()
                });
            }
            Reader.Close();
            Connection.Close();
            return aSells;
        }

        public int DeleteInvoiceById(int id)
        {
            Query = "UPDATE SellItems SET status='delete' WHERE BillNo='" + id + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowAffected > 0)
            {
                Query = "UPDATE Sell SET status='delete' WHERE Id='" + id + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
            }
            return rowAffected; 
        }
    }
}