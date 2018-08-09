using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Motskin.BLL;
using arkAS.Areas.Motskin.Models;
using arkAS.Models;
using Newtonsoft.Json;

namespace arkAS.Areas.Motskin.Controllers
{
    public class ContractorController : BaseController
    {
        public ContractorController(IManager mng) : base(mng)
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
            string msg;
            var total = 0;
            var items = mng.Contractors.GetItems(parameters, user, out msg, out total);

            var json = JsonConvert.SerializeObject(new
            {
                items,
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult Contractors_save()
        {
            string msg;
            var parameters = ConvertToSimpleDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            arkAS.BLL.motskin_contractors item;


            if (RDL.Convert.StrToInt(parameters["id"].ToString(), 0) == 0)
            {
                item = mng.Contractors.Create(parameters, user, out msg);
            }
            else
            {
                item = mng.Contractors.Edit(parameters, user, out msg);
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

        //public ActionResult ContractorsInLine(int pk, string value, string name)
        //{
        //    string msg;
        //    var parameters = AjaxModel.GetAjaxParameters(HttpContext);
        //    var user = mng.GetUser();
        //    var item = mng.Contractors.Create(parameters, user, out msg);

        //    if (msg != "")
        //    {
        //        return Json(new
        //        {
        //            result = false,
        //            msg = msg
        //        });
        //    }
        //    else
        //    {
        //        return Json(new { result = true});
        //    }
        //}

        public ActionResult Contractors_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            string msg;
            result = mng.Contractors.Delete(id, user, out msg);

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