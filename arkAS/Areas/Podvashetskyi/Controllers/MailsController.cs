using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Podvashetskyi.Controllers
{
    public class MailsController : BaseController
    {
        public MailsController() 
            : base (new Manager(new PdvDocumentRepository(new LocalSqlServer())))
        {

        }
        // GET: Podvashetskyi/Mails
        public ActionResult Index()
        {
            return View();
        }
        // Службы доставки
        public ActionResult DeliveryServices()
        {
            return View();
        }
        public ActionResult DeliveryServices_getItems()
        {
            var items = mng.Mails.GetDeliveryServices();
            return Json(new
            {
                items = items.Select(x => new
                {
                    x.id,
                    x.name,
                    x.code
                }),
                total = items.Count
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeliveryServices_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newDeliveryService = new pdv_deliveryService
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    code = AjaxModel.GetValueFromSaveField("code", fields)
                };

                mng.Mails.SaveDeliveryService(newDeliveryService);
                return Json(new
                {
                    result = true,
                    id = newDeliveryService.id,
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
        public ActionResult DeliveryServices_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Mails.DeleteDeliveryService(int.Parse(id));
                return Json(new
                {
                    result = res,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    msg = msg
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeliveryServicesInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Mails.EditDeliveryServiceField(pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
        // статусы доставки 
        public ActionResult DeliveryMailStatuses()
        {
            return View();
        }
        public ActionResult DeliveryMailStatuses_getItems()
        {
            var items = mng.Mails.GetDeliveryMailStatuses();
            return Json(new
            {
                items = items.Select(x => new
                {
                    x.id,
                    x.name,
                    x.code
                }),
                total = items.Count
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeliveryMailStatuses_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newDeliveryMailStatus = new pdv_deliveryMailStatuses
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    code = AjaxModel.GetValueFromSaveField("code", fields)
                };

                mng.Mails.SaveDeliveryMailStatus(newDeliveryMailStatus);
                return Json(new
                {
                    result = true,
                    id = newDeliveryMailStatus.id,
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
        public ActionResult DeliveryMailStatuses_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Mails.DeleteDeliveryMailStatus(int.Parse(id));
                return Json(new
                {
                    result = res,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
                return Json(new
                {
                    result = false,
                    msg = msg
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeliveryMailStatusesInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Mails.EditDeliveryMailStatusField(pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
    }
}