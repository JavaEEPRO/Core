using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  arkAS.Areas.Motskin.BLL;
using arkAS.Areas.Motskin.Models;
using arkAS.Models;
using Newtonsoft.Json;

namespace arkAS.Areas.Motskin.Controllers
{
    public class MailController : BaseController
    {
        public MailController(IManager mng) : base(mng)
        {
            
        }

        public ActionResult Mails()
        {
            MailsViewModel model = new MailsViewModel();
            model.System = mng.Mail.GetSystems();
            model.Statuses = mng.Mail.GetStatuses();
            return View(model);
        }

        public ActionResult Mails_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg;
            var total = 0;
            var items = mng.Mail.GetItems(parameters, user, out msg, out total);

            var json = JsonConvert.SerializeObject(new
            {
                items,
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult ChangeMailStatus(int pk, string value, string name)
        {
            string msg;
            var result = false;
            var user = mng.GetUser();
            int newStatus = int.Parse(value);
            result = mng.Mail.ChangeStatus(pk, value, name, user, out msg);
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
            string msg;
            var parameters = ConvertToSimpleDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            arkAS.BLL.motskin_mails item;

            if (RDL.Convert.StrToInt(parameters["id"].ToString(), 0) == 0)
            {
                item = mng.Mail.Create(parameters, user, out msg);
            }
            else
            {
                item = mng.Mail.Edit(parameters, user, out msg);
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

         public ActionResult Mails_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            string msg;
            result = mng.Mail.Delete(id, user, out msg);

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

        public ActionResult LogMailStatuses(int id)
        {
            var items = mng.Mail.GetLogStatus(id);
            var json = JsonConvert.SerializeObject(new
            {
                items = items.OrderBy(x => x.created).Select(x => new
                {
                    status = x.motskin_mailStatuses.name,
                    created = x.created.ToString("dd.MM.yyyy"),
                    x.note
                })
            });
            return Content(json, "application/json");
        }
    }
}