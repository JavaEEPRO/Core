using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Motskin.BLL;
using arkAS.Areas.Motskin.Models;
using arkAS.Models;
using Newtonsoft.Json;

namespace arkAS.Areas.Motskin.Controllers
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(IManager mng) : base(mng)
        {
            
        }

        public ActionResult Invoices()
        {
            InvoiceViewModel model = new InvoiceViewModel();
            model.Contractors = mng.Contractors.GetAll();
            model.Statuses = mng.Invoices.GetStatuses();
            return View(model);
        }

        public ActionResult Invoices_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg;
            var total = 0;
            var items = mng.Invoices.GetItems(parameters, user, out msg, out total);

            var json = JsonConvert.SerializeObject(new
            {
                items,
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult ChangeInvoiceStatus(int pk, string value, string name)
        {
            string msg;
            var result = false;
            var user = mng.GetUser();
            int newStatus = int.Parse(value);
            result = mng.Invoices.ChangeStatus(pk, value, name, user, out msg);
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
            string msg;
            var parameters = ConvertToSimpleDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            arkAS.BLL.motskin_invoices item;

            if (RDL.Convert.StrToInt(parameters["id"].ToString(), 0) == 0)
            {
                item = mng.Invoices.Create(parameters, user, out msg);
            }
            else
            {
                item = mng.Invoices.Edit(parameters, user, out msg);
            }

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

        public ActionResult Invoices_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            string msg;
            result = mng.Invoices.Delete(id, user, out msg);

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

        public ActionResult LogInvoiceStatuses(int id)
        {
            var items = mng.Invoices.GetLogStatus(id);
            var json = JsonConvert.SerializeObject(new
            {
                items = items.OrderBy(x => x.created).Select(x => new
                {
                    status = x.motskin_invoiceStatuses.name,
                    created = x.created.ToString("dd.MM.yyyy"),
                    x.note
                })
            });
            return Content(json, "application/json");
        }
    }
}