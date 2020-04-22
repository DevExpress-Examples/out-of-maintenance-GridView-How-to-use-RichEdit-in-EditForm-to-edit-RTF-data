using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit;
using System;
using System.Text;
using System.Web.Mvc;
using WebApplicationCS.Models;

namespace WebApplicationCS.Controllers {
    public class HomeController : Controller {
		
        public ActionResult Index() {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RichEditPartial(string documentID) {
            RichEditModel model = new RichEditModel();
            model.documentID = documentID;
            model.rtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf));
            return PartialView("_RichEditPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial() {
            var model = OleDbDataProvider.GetCars();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Car item) {
            if (ModelState.IsValid) {
                try {
                    item.RtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf));
                    //Note that data modifications are not allowed in online demos. 
                    //To allow editing in local/offline mode, download the example 
                    //and comment out the line below and in GridViewPartialUpdate action method respectively.

                    //OleDbDataProvider.AddNewItem(item);
                    ViewData["EditError"] = "Data modifications are not allowed in online demos";
                }
                catch (Exception e) {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";           
            var model = OleDbDataProvider.GetCars();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Car item) {
            if (ModelState.IsValid) {
                try {
                    item.RtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf));
                    //OleDbDataProvider.UpdateItem(item);
                    ViewData["EditError"] = "Data modifications are not allowed in online demos";
                }
                catch (Exception e) {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = OleDbDataProvider.GetCars();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(Int32 ID) {
            if (ID >= 0) {
                try {
                    OleDbDataProvider.DeleteItem(ID);
                }
                catch (Exception e) {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = OleDbDataProvider.GetCars();
            return PartialView("_GridViewPartial", model);
        }
    }
}