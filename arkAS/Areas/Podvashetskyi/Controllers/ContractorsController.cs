using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Podvashetskyi.Controllers
{
    public class ContractorsController : BaseController
    {
        public ContractorsController()
            : base(new Manager(new PdvDocumentRepository(new LocalSqlServer())))
        {

        }
        // GET: Podvashetskyi/Contractors
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contractors_getItems()
        {
            var user = mng.GetUser();
            var msg = "";

            var items = mng.Contractors.GetContractors(user, out msg);
            if (items != null)
            {
                return Json(new
                {
                    items = items.Select(x => new
                    {
                        x.id,
                        x.name,
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
        public ActionResult Contractors_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newContractor = new pdv_contractors
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    // isDeleted = AjaxModel.GetValueFromSaveField("isDelete", fields)
                };

                mng.Contractors.SaveContractor(newContractor);
                return Json(new
                {
                    result = true,
                    id = newContractor.id,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    id = 0,
                    msg = "Ошибка"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Contractors_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Contractors.DeleteContractor(int.Parse(id),out msg);
                return Json(new
                {
                    msg = msg,
                    result = res,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    msg = msg,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ContractorsInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Contractors.EditContractorsField(pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
    }
}