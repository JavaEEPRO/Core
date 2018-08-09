using arkAS.Areas.Vasilyev.BLL;
using arkAS.BLL;
using arkAS.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arkAS.Areas.Vasilyev.Controllers
{
    public class ContractorController : BaseController
    {
        public ContractorController(IManager mng)
            : base(mng)
        {
        }
        public ActionResult Contractors()
        {
            return View();
        }
        public ActionResult Contractors_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            var msg = "";
            var items = mng.Contractor.GetContractors(parameters, user, out msg);

            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.name,
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
        public ActionResult CreateContractor(string name)
        {
            var user = mng.GetUser();
            var msg = "";
            var item = mng.Contractor.CreateContractor(name, user, out msg);

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
                return Json(new { result = true,  itemID = item.id });
            }
        }
        public ActionResult ContractorsInLine(int pk, string value, string name)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Contractor.EditContractorField(pk, name, value, user, out msg);

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
        public ActionResult Contractors_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            var msg = "";
            result = mng.Contractor.DeleteContractor(id, user, out msg);

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