using System.Linq;
using System.Web.Mvc;
using arkAS.Areas.Tsilurik.BLL;
using arkAS.Models;
using arkAS.Areas.Tsilurik.Models;

namespace arkAS.Areas.Tsilurik.Controllers
{
    public class MailController : BaseController
    {
        public MailController(IManager mng) : base(mng)
        {
        }
        public ActionResult Mails()
        {
            var model  = mng.Mail.GetMailStatuses();
            return View(model);
        }
        public ActionResult Mails_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var total = 0;
            var items = mng.Mail.GetMails(parameters, user, out msg,out total);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.trackNumber,
                        date = x.date.ToString("dd.MM.yyyy"),
                        x.from,
                        x.to,
                        status = x.tsilurik_status != null ? x.tsilurik_status.name : "",
                        x.description,
                        x.mailSystem,
                        returnDate = x.returnDate != null ? x.returnDate.GetValueOrDefault().ToString("dd.MM.yyyy") : "Нет даты",
                        returnTrackNumber = x.returnTrackNumber != null ? x.returnTrackNumber : "Нет номера",

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

        public ActionResult ChangeMailStatus(int pk, string value, string name)
        {
            var msg = "";
            var result = false;
            var user = mng.GetUser();
            result = mng.Mail.ChangeMailStatus(pk, value, name, user, out msg);
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

        public ActionResult Mails_save()
        {
            var msg = "";
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var user = mng.GetUser();
            var item = mng.Mail.CreateMail(parameters, user, out msg);

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