using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Molchanov.BLL;
using Newtonsoft.Json;
using arkAS.BLL;
using arkAS.Models;
using arkAS.Areas.Molchanov.Models;

namespace arkAS.Areas.Molchanov.Controllers
{
    [Authorize(Roles = "Admin, Guest")]
    public class InvoiceController : BaseController
    {
        public InvoiceController(IManager mng) : base(mng)
        {
        }
        #region Invoices
        public ActionResult InvoicesList()
        {
            var user = mng.GetUser();
            string msg = "";
            var model = new InvoiceViewModel
            {
                Contragents = mng.Contragents.GetContragents(out msg, user),
                invStatuses = mng.Invoices.GetInvoiceStatuses(out msg, user)
            };

            return View(model);
        }
        public ActionResult InvoicesList_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg = "";
            var items = mng.Invoices.GetInvoices(out msg, user);     
            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var text = parameters.filter.ContainsKey("text") ? parameters.filter["text"].ToString() : "";
                if (text != "")
                {
                    items = items.Where(x => x.number != null && x.number.Contains(text)).ToList();
                }
            }
            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            switch (sort1)
            {
                case "number":
                    if (direction1 == "up") items = items.OrderBy(x => x.number).ToList();
                    else items = items.OrderByDescending(x => x.number).ToList();
                    break;
                case "email":
                    if (direction1 == "up") items = items.OrderBy(x => x.date).ToList();
                    else items = items.OrderByDescending(x => x.date).ToList();
                    break;
                default:
                    if (direction1 == "up") items = items.OrderBy(x => x.number).ToList();
                    else items = items.OrderByDescending(x => x.number).ToList();
                    break;
            }
            var total = items.Count();
            var res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new
                {
                    id = x.id,
                    uniqueCode = x.uniqueCode,
                    date = x.date.ToShortDateString(),
                    number = x.number,
                    description = x.description,
                    isDeleted = x.isDeleted,
                    contragentName = x.molchanov_contragents.name,
                    invoiceStatus = x.molchanov_invoiceStatuses.name
                }),
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }
        public ActionResult InvoicesList_save()
        {
            var parameters = CRUDToDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            string msg = "";
            molchanov_invoices item;
            int currentID = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);
            if (currentID == 0)
            {
                item = mng.Invoices.CreateInvoice(parameters, out msg, user);
            }
            else
            {
                item = mng.Invoices.EditInvoice(parameters, currentID, out msg, user);
            }
            var json = JsonConvert.SerializeObject(new { result = true, msg });
            return Content(json, "application/json");
        }
        public ActionResult InvoicesList_remove(int id)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            result = mng.Invoices.RemoveInvoice(id, out msg, user);
            if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result });
            }
        }
        public ActionResult ChangeInvStatus(int pk, string value, string name )
        {
            var user = mng.GetUser();
            string msg = "";
            var result = mng.Invoices.ChangeInvoicesStatus(pk, name, value, out msg, user);
            var json = JsonConvert.SerializeObject(new { result = result});
            return Content(json, "application/json");
        }
        #endregion
        #region InvoiceStatuses
        public ActionResult InvoiceStatusesList()
        {
            return View();
        }
        public ActionResult InvoiceStatusesList_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg = "";
            var items = mng.Invoices.GetInvoiceStatuses(out msg, user);
            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var text = parameters.filter.ContainsKey("text") ? parameters.filter["text"].ToString() : "";
                if (text != "")
                {
                    items = items.Where(x => x.name != null && x.name.Contains(text)).ToList();
                }
            }
            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            switch (sort1)
            {
                case "name":
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
                case "email":
                    if (direction1 == "up") items = items.OrderBy(x => x.code).ToList();
                    else items = items.OrderByDescending(x => x.code).ToList();
                    break;
                default:
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
            }
            var total = items.Count();
            var res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new {
                    id = x.id,
                    name = x.name,
                    code = x.code
                }),
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }
        public ActionResult InvoiceStatusesList_save()
        {
            var parameters = CRUDToDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            string msg = "";
            molchanov_invoiceStatuses item;
            int currentID = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);
            if (currentID == 0)
            {
                item = mng.Invoices.CreateInvoiceStatus(parameters, out msg, user);
            }
            else
            {
                item = mng.Invoices.EditInvoiceStatus(parameters, currentID, out msg, user);
            }
            var json = JsonConvert.SerializeObject(new { result = true, msg });
            return Content(json, "application/json");
        }
        public ActionResult InvoiceStatusesList_remove(int id)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            result = mng.Invoices.RemoveInvoiceStatus(id, out msg, user);
            result = mng.Invoices.RemoveInvoice(id, out msg, user);
            if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result });
            }
        }
        #endregion
    }
}