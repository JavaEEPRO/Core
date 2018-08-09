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
    public class DocumentController : BaseController
    {
        public DocumentController(IManager mng) : base(mng)
        {
        }
        public ActionResult Documents()
        {
            DocumentViewModel vm = new DocumentViewModel();
            vm.Statuses = mng.Document.GetDocStatuses();
            vm.Types = mng.Document.GetDocTypes();
            vm.Contractors = mng.Contractor.GetContractors();
            return View(vm);
        }
        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var items = mng.Document.GetDocuments(parameters, user, out msg);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.name,
                        x.number,
                        date = x.date.ToString("dd.MM.yyyy"),
                        type = x.vas_docTypes != null ? x.vas_docTypes.name : "",
                        status = x.vas_docStatuses != null ? x.vas_docStatuses.name : "",
                        contractor = x.vas_contractors != null ? x.vas_contractors.name : "",
                        sum = x.sum.ToString(),
                        x.link,
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
        public ActionResult CreateDocument(string name, int typeID, int contractorID)
        {
            var user = mng.GetUser();
            var msg = "";
            var item = mng.Document.CreateDocument(name, typeID, contractorID, user, out msg);

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
        public ActionResult DocumentsInLine(int pk, string value, string name)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Document.EditDocumentField(pk, name, value, user, out msg);

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
        public ActionResult Documents_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Document.DeleteDocument(id, user, out msg);

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