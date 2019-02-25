using System.Web.Mvc;
using StockInventorySystem.Manager;
using StockInventorySystem.Models;

namespace StockInventorySystem.Controllers
{
    public class DeleteController : Controller
    {
        PurchaseManager aPurchaseManager=new PurchaseManager();
        DeleteManager aDeleteManager=new DeleteManager();
        //
        // GET: /Delete/
        public ActionResult Item()
        {
            ViewBag.AllItem = aPurchaseManager.GetAllItems();
            return View();
        }
        [HttpPost]
        public ActionResult Item(Item aItem)
        {
            string msg = aDeleteManager.DeleteItemById(aItem);
            ViewBag.Message = msg;
            ViewBag.AllItem = aPurchaseManager.GetAllItems();


            return View();
        }
        public ActionResult Supplier()
        {
            ViewBag.Supplier = aPurchaseManager.GetAllSupplier();
            return View();
        }
        [HttpPost]
        public ActionResult Supplier(Supplier aSupplier)
        {
            string msg = aDeleteManager.DeleteSupplierById(aSupplier);
            ViewBag.Message = msg;
            ViewBag.Supplier = aPurchaseManager.GetAllSupplier();
            return View();
        }
	}
}