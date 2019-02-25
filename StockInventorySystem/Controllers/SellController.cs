using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockInventorySystem.Manager;
using StockInventorySystem.Models;

namespace StockInventorySystem.Controllers
{
    public class SellController : Controller
    {
        PurchaseManager aPurchaseManager = new PurchaseManager();
        SellManager aSellManager = new SellManager();
        //
        // GET: /Sell/
        public ActionResult Save()
        {
            string billNo = aSellManager.GetBillNo();
            ViewBag.BillNo = billNo;
            List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            ViewBag.Supplier = aSuppliers;
            List<Item> aItems = aPurchaseManager.GetAllItems();
            ViewBag.Items = aItems;
            return View();
        }
        [HttpPost]
        public JsonResult CheckItem(SellItems aSell)
        {
            string status = null;
            //Global.Id = O.InvoiceNo;
            string message = aSellManager.CheckItems(aSell);
            if (message == "yes")
            {
                status = message;
                ModelState.Clear();
            }
            else if (message == "no")
            {
                status = message;
            }
            else
            {
                status = message;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult SaveOrder(Sell aSell)
        {
            string status = null;
            Global.Id = aSell.BillNo;
            string message = aSellManager.Save(aSell);
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
        public ActionResult Successful()
        {
            string NestId = Global.Id;
            ViewBag.NestId = NestId;
            return View();
        }
        public ActionResult GeneratePdfPreview()
        {
            string NestId = Global.Id;
            List<Sell> aSells = aSellManager.GetItemsByBillNo(NestId);
            ViewBag.Sell = aSells[0];
            ViewBag.Id = NestId;

            return View();
        }

        public ActionResult GeneratePdf()
        {

            return new Rotativa.ActionAsPdf("GeneratePdfPreview");
        }
        public ActionResult Query()
        {
            List<Sell> billNo = aSellManager.GetAllBillNo();
            ViewBag.BillNo = billNo;
            //List<Supplier> aSuppliers = aPurchaseManager.GetAllSupplier();
            //ViewBag.Supplier = aSuppliers;
            return View();
        }

        public JsonResult GetAllSellItem(string billNo)
        {

            Global.Id = billNo;
            var aSells = aSellManager.GetItemsByBillNo(billNo).ToList();
            return Json(aSells, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePdfPreviewQuery()
        {
            string NestId = Global.Id;
            List<Sell> aSells = aSellManager.GetItemsByBillNo(NestId);
            ViewBag.Sell = aSells[0];
            ViewBag.Id = NestId;

            return View();
        }
        public ActionResult GeneratePdfQuery()
        {

            return new Rotativa.ActionAsPdf("GeneratePdfPreviewQuery");
        }
        public ActionResult Delete()
        {
            List<Sell> billNo = aSellManager.GetAllBillNo();
            ViewBag.BillNo = billNo;

            return View();
        }
        [HttpPost]
        public ActionResult Delete(Sell aSell)
        {
            string message = aSellManager.DeleteInvoiceById(aSell.Id);
            ViewBag.Message = message;
            List<Sell> billNo = aSellManager.GetAllBillNo();
            ViewBag.BillNo = billNo;
           
            if (message == "Yes")
            {
                ModelState.Clear();
            }
            return View();
        }
    }
}