using System.Linq;
using System.Web.Mvc;
using arkAS.Areas.Tsilurik.BLL;
using arkAS.Models;
using arkAS.Areas.Tsilurik.Models;

namespace arkAS.Areas.Tsilurik.Controllers
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(IManager mng) : base(mng)
        {
        }
        public ActionResult Invoices()
        {
            InvoiceViewModel model = new InvoiceViewModel();
            model.Contractors = mng.Contractor.GetContractors();
            model.Statuses = mng.Invoice.GetInvoiceStatuses();
            return View(model);
        }
        public ActionResult Invoices_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var total = 0;
            var items = mng.Invoice.GetInvoices(parameters,user, out msg,out total);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.number,
                        date = x.date.ToString("dd.MM.yyyy"),
                        status = x.tsilurik_status != null ? x.tsilurik_status.name : "",
                        contractor = x.tsilurik_contractors != null ? x.tsilurik_contractors.name : "",
                        x.note,

                    }),
                    total = total,
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

        public ActionResult ChangeInvoiceStatus(int pk, string value, string name)
        {
            var msg = "";
            var result = false;
            var user = mng.GetUser();
            var res = mng.Invoice.ChangeInvoiceStatus(pk, value, name, user, out msg);
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

        public ActionResult Invoices_save()
        {
            var msg = "";
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var user = mng.GetUser();
            var item = mng.Invoice.CreateInvoice(parameters, user, out msg);

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
    }
}