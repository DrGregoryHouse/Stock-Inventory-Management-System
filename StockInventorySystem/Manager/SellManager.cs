using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockInventorySystem.Gateway;
using StockInventorySystem.Models;

namespace StockInventorySystem.Manager
{
    public class SellManager
    {
        SellGateway  aSellGateway=new SellGateway();
        public string GetBillNo()
        {
            return aSellGateway.GetBillNo();
        }

        public string CheckItems(SellItems aSell)
        {
            return aSellGateway.CheckItems(aSell);
        }


        public string Save(Sell aSell)
        {
            bool doesExists = aSellGateway.DoesExists(aSell.BillNo);
            if (doesExists)
            {
                return "Exists";
            }
            int rowAffected = aSellGateway.Save(aSell);
            if (rowAffected > 0)
            {
                return "Yes";
            }
            return "No";
        }

        public List<Sell> GetItemsByBillNo(string nestId)
        {
            return aSellGateway.GetItemsByBillNo(nestId);
        }

        public List<Sell> GetAllBillNo()
        {
            return aSellGateway.GetAllBillNo();
        }

        public string DeleteInvoiceById(int id)
        {
            int rowAffected = aSellGateway.DeleteInvoiceById(id);
            if (rowAffected > 0)
            {
                return "Yes";
            }
            return "No";
        }
    }
}