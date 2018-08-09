using arkAS.Areas.Vasilyev.BLL;
using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arkAS.Areas.Vasilyev.Controllers
{
    public class MailController : BaseController
    {
        public MailController(IManager mng)
            : base(mng)
        {
        }
        public ActionResult Mails()
        {
            var model = mng.Mail.GetMailStatuses();
            return View(model);
        }
        public ActionResult Mails_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var items = mng.Mail.GetMails(parameters, user, out msg);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.from,
                        x.to,
                        x.trackNumber,
                        x.mailSystem,
                        date = x.date.ToString("dd.MM.yyyy"),
                        trackNumberRepay = x.trackNumberRepay != null ? x.trackNumberRepay : "Нет номера",
                        dateReplay = x.dateReplay != null ? x.dateReplay.GetValueOrDefault().ToString("dd.MM.yyyy") : "Нет даты",
                        status = x.vas_mailStatuses != null ? x.vas_mailStatuses.name : "",
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
        public ActionResult CreateMail(string from, string to)
        {
            var user = mng.GetUser();
            var msg = "";
            var item = mng.Mail.CreateMail(from, to, user, out msg);

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
        public ActionResult MailsInLine(int pk, string value, string name)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Mail.EditMailField(pk, name, value, user, out msg);

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
        public ActionResult Mails_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Mail.DeleteMail(id, user, out msg);

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