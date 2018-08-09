using System.Linq;
using System.Web.Mvc;
using arkAS.Areas.Tsilurik.BLL;
using arkAS.Models;
using arkAS.Areas.Tsilurik.Models;
using System.Web.Security;

namespace arkAS.Areas.Tsilurik.Controllers
{
    public class DocumentController : BaseController
    {
        public DocumentController(IManager mng) : base(mng)
        {
        }
        public ActionResult Documents()
        {
            DocumentViewModel model = new DocumentViewModel();
            model.Types = mng.Document.GetDocumentTypes();
            model.Contractors = mng.Contractor.GetContractors();
            model.Statuses = mng.Document.GetDocumentStatuses();
            return View(model);
        }
        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var total=0;
            var items = mng.Document.GetDocuments(parameters, user, out msg,out total);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.number,
                        date = x.date.ToString("dd.MM.yyyy"),
                        type = x.tsilurik_types != null ? x.tsilurik_types.name : "",
                        status = x.tsilurik_status != null ? x.tsilurik_status.name : "",
                        contractor = x.tsilurik_contractors != null ? x.tsilurik_contractors.name : "",
                        sum = x.sum.ToString(),
                        x.link,
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

        public ActionResult ChangeDocumentStatus(int pk, string value, string name)
        {
            var msg = "";
            var result=false;
            var user = mng.GetUser();
            result = mng.Document.ChangeDocumentStatus(pk, value, name, user, out msg);
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

        public ActionResult Documents_save()
        {
            var msg = "";
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var user = mng.GetUser();
            var item = mng.Document.CreateDocument(parameters,user,out msg);

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