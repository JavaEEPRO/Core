using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Podvashetskyi.Controllers
{
    public class InvoicesController : BaseController
    {
        public InvoicesController() 
            : base (new Manager(new PdvDocumentRepository(new LocalSqlServer())))
        {

        }
        // GET: Podvashetskyi/Invoices
        public ActionResult Index()
        {
            return View();
        }
        // Invoices Status
        public ActionResult InvoicesStatuses()
        {
            return View();
        }
        public ActionResult InvoicesStatuses_getItems()
        {
            var items = mng.Invoices.GetInvoicesStatuses();
            return Json(new
            {
                items = items.Select(x => new
                {
                    x.id,
                    x.name,
                    x.code
                }),
                total = items.Count
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InvoicesStatuses_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newInvoicesStatus = new pdv_invoiceStatuses
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    code = AjaxModel.GetValueFromSaveField("code", fields)
                };

                mng.Invoices.SaveInvoicesStatus(newInvoicesStatus);
                return Json(new
                {
                    result = true,
                    id = newInvoicesStatus.id,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    id = 0,
                    msg = "Ошибка"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InvoicesStatuses_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Invoices.DeleteInvoiceStatus(int.Parse(id));
                return Json(new
                {
                    result = res,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    msg = msg
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InvoicesStatusesInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Invoices.EditInvoiceStatusField(pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
    }
}