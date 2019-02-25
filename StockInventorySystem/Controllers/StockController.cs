using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rotativa;
using StockInventorySystem.Manager;
using StockInventorySystem.Models;

namespace StockInventorySystem.Controllers
{
    public class StockController : Controller
    {
        PurchaseManager aPurchaseManager = new PurchaseManager();
        //
        // GET: /Stock/
        public ActionResult Index()
        {
            string aPurchase = aPurchaseManager.GetInvoiceNo();
            ViewBag.InvoiceNo = aPurchase;
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            List<Item> aItems = aPurchaseManager.GetAllItems();
            ViewBag.Items = aItems;
            ViewBag.Count = aItems.Count;
            return View();
        }
        [HttpPost]
        public JsonResult SaveOrder(Purchase order)
        {
            string status = null;
            Global.Id = order.InvoiceNo;
            string message = aPurchaseManager.Save(order);
            if (message == "Yes")
            {
                status = message;
                ModelState.Clear();
            }
            else if (message == "Exists")
            {
                status = message;
            }
            else
            {
                status = message;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult Details()
        {
            string NestId = Global.Id;
            ViewBag.NestId = NestId;
            return View();
        }
        public ActionResult GeneratePdfPreview()
        {
            string NestId = Global.Id;
            List<Purchase> aPurchase = aPurchaseManager.GetItemsByInvoiceNo(NestId);
            ViewBag.Purchase = aPurchase[0];
            ViewBag.Id = NestId;

            return View();
        }

        public ActionResult GeneratePdf()
        {

            return new ActionAsPdf("GeneratePdfPreview");
        }

        public ActionResult Update()
        {
            List<Purchase> aPurchases = aPurchaseManager.GetAllInvoiceNo();
            ViewBag.InvoiceId = aPurchases;
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            return View();
        }
        public ActionResult Query()
        {
            List<Purchase> aPurchases = aPurchaseManager.GetAllInvoiceNo();
            ViewBag.InvoiceId = aPurchases;
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            return View();
        }

        public JsonResult GetAllPurchaseItem(string invoiceNo)
        {
            Global.Id = invoiceNo;
            var aPurchases = aPurchaseManager.GetItemsByInvoiceNo(invoiceNo).ToList();
            return Json(aPurchases, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePdfPreviewQuery()
        {
            string NestId = Global.Id;
            List<Purchase> aPurchase = aPurchaseManager.GetItemsByInvoiceNo(NestId);
            ViewBag.Purchase = aPurchase[0];
            ViewBag.Id = NestId;

            return View();
        }
        public ActionResult GeneratePdfQuery()
        {

            return new ActionAsPdf("GeneratePdfPreviewQuery");
        }
        public ActionResult Delete()
        {
            List<Purchase> aPurchases = aPurchaseManager.GetAllInvoiceNo();
            ViewBag.InvoiceId = aPurchases;

            return View();
        }
        [HttpPost]
        public ActionResult Delete(Purchase aPurchase)
        {
            string message = aPurchaseManager.DeleteInvoiceById(aPurchase.Id);
            ViewBag.Message = message;
            List<Purchase> aPurchases = aPurchaseManager.GetAllInvoiceNo();
            ViewBag.InvoiceId = aPurchases;

            if (message == "Yes")
            {
                ModelState.Clear();
            }
            return View();
        }

        
    }

}