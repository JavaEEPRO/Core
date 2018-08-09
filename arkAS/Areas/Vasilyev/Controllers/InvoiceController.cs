using arkAS.Areas.Vasilyev.BLL;
using arkAS.Areas.Vasilyev.Models;
using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arkAS.Areas.Vasilyev.Controllers
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(IManager mng)
            : base(mng)
        {
        }
        public ActionResult Invoices()
        {
            InvoiceViemModel vm = new InvoiceViemModel();
            vm.Statuses = mng.Invoice.GetInvoiceStatuses();
            vm.Contractors = mng.Contractor.GetContractors();
            return View(vm);
        }
        public ActionResult Invoices_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var items = mng.Invoice.GetInvoices(parameters, user, out msg);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.number,
                        date = x.date.ToString("dd.MM.yyyy"),
                        status = x.vas_invoiceStatuses != null ? x.vas_invoiceStatuses.name : "",
                        contractor = x.vas_contractors != null ? x.vas_contractors.name : "",
                        x.comment,
                        x.code
                    }),
                    total = items.Count,
                    msg = msg,
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    items = 0,
                    total = 0,
                    msg = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CreateInvoice(int contractorID)
        {
            var user = mng.GetUser();
            var msg = "";
            var item = mng.Invoice.CreateInvoice(contractorID, user, out msg);

            if (msg != "")
            {
                return Json(new
                {
                    result = false,
                    msg = msg
                });
            }
            else
            {
                return Json(new { result = true, itemID = item.id });
            }
        }
        public ActionResult InvoicesInLine(int pk, string value, string name)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Invoice.EditInvoiceField(pk, name, value, user, out msg);

            if (!result)
            {
                return Json(new
                {
                    result = result,
                    msg = msg
                });
            }
            else
            {
                return Json(new { result = result });
            }
        }
        public ActionResult Invoices_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Invoice.DeleteInvoice(id, user, out msg);

            if (!result)
            {
                return Json(new
                {
                    result = result,
                    msg = msg
                });
            }
            else
            {
                return Json(new { result = result });
            }
        }
    }
}