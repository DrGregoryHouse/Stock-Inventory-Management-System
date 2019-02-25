using System.Collections.Generic;
using System.Web.Mvc;
using StockInventorySystem.Manager;
using StockInventorySystem.Models;

namespace StockInventorySystem.Controllers
{
    public class InsertController : Controller
    {
        //
        // GET: /Insert/
        PurchaseManager aPurchaseManager=new PurchaseManager();
        InsertManager aInsertManager = new InsertManager();
        public ActionResult ItemInsert()
        {
            List<Item> aItems = aPurchaseManager.GetAllItems();
            ViewBag.Item = aItems;
            return View();
        }
        [HttpPost]
        public ActionResult ItemInsert(Item aItem)
        {
            ViewBag.msg = aInsertManager.SaveItem(aItem);
            List<Item> aItems = aPurchaseManager.GetAllItems();
            ViewBag.Item = aItems;
            return View();
        }

        public ActionResult InsertSupplier()
        {
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            return View();
        }

        [HttpPost]
        public ActionResult InsertSupplier(Supplier aSupplier)
        {
            ViewBag.msg = aInsertManager.SaveSupplier(aSupplier);
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            return View();
        }

        
    }
}