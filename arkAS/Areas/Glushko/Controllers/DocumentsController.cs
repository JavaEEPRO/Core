using arkAS.Areas.Glushko.BLL;
using arkAS.BLL;
using System.Data;
using arkAS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL.Core;
using System.Web.Mvc;
using Newtonsoft.Json;
using Dapper;

namespace arkAS.Areas.Glushko.Controllers
{
    public class DocumentsController : BaseController
    {

        public DocumentsController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        // GET: Glushko/Documents
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocStatuses()
        {
            return View();
        }

        public ActionResult Documents()
        {
            ViewBag.DocTypes = mng.Documents.GetDocumentTypes();
            ViewBag.DocStatuses = mng.Documents.GetDocumentStatuses();
            ViewBag.FinContragents = mng.Documents.GetFinContragents();
            return View();
        }


        public ActionResult CreateDocument(string name, string number, string path, int typeID, int contragentID)
        {
            int statusID = 3;
            var item = new gl_docs
            {
                id = 0,
                name = name,
                number = number,
                path = path,
                docTypeID = typeID,
                docStatusID = statusID,
                contragentID = contragentID,
                created = DateTime.Now.Date,
                createdBy = new CoreManager().GetUserGuid(),
                parentItemID = null
            };
            mng.Documents.SaveDocument(item);
            return Json(new
            {
                result = item.id > 0,
                savedID = item.id,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);

            var name = "";
            var number = "";
            var path = "";
            var typeID = "";
            var statusID = "";
           // var projectID = "";
            var contragentID = "";
            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                name = parameters.filter.ContainsKey("name") ? parameters.filter["name"].ToString() : "";
                number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                path = parameters.filter.ContainsKey("path") ? parameters.filter["path"].ToString() : "";
                typeID = parameters.filter.ContainsKey("typeID") ? parameters.filter["typeID"].ToString() : "0";
                statusID = parameters.filter.ContainsKey("statusID") ? parameters.filter["statusID"].ToString() : "0";
               // projectID = parameters.filter.ContainsKey("projectID") ? parameters.filter["projectID"].ToString() : "0";
                contragentID = parameters.filter.ContainsKey("contragentID") ? parameters.filter["contragentID"].ToString() : "0";

                if (parameters.filter.ContainsKey("created") && parameters.filter["created"] != null)
                {
                    var dates = parameters.filter["created"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (dates.Length > 0)
                    {
                        createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                    }
                    if (dates.Length > 1)
                    {
                        createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                        createdMax = createdMax.AddDays(1).AddSeconds(-1);
                    }
                }
            }

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var rep = new CoreRepository();
            var p = new DynamicParameters();
            p.Add("name", name);
            p.Add("number", number);
            p.Add("path", path);
            p.Add("typeID", typeID);
            p.Add("statusID", statusID);
           // p.Add("projectID", projectID);
            p.Add("contragentID", contragentID);
            p.Add("createdMin", createdMin);
            p.Add("createdMax", createdMax);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("GL_GetDocs", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult Documents_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            res = mng.Documents.DeleteDocument(int.Parse(id), out msg);
            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DocumentsInline(int pk, string value, string name)
        {
            string msg = "";
            mng.Documents.EditDocumentField(pk, name, value,out msg);
            return Json(new
            {
                result = true
            });
        }

        public ActionResult DocStatuses_getItems()
        {
            var items = mng.Documents.GetDocumentStatuses();
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

        public ActionResult DocStatuses_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newDocType = new gl_docStatuses
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    code = AjaxModel.GetValueFromSaveField("code", fields)
                };

                mng.Documents.SaveDocumentStatus(newDocType);
                return Json(new
                {
                    result = true,
                    id = newDocType.id,
                    msg = "Операция успешна"
                }, JsonRequestBehavior.AllowGet);           
        }

        public ActionResult DocStatuses_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";            
                res = mng.Documents.DeleteDocumentStatus(int.Parse(id), out msg);
                return Json(new
                {
                    result = res,
                    msg = msg
                }, JsonRequestBehavior.AllowGet);                     
           
        }

        public ActionResult DocStatusesInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Documents.EditDocumentStatusField(pk, name, value, out msg);

            return Json(new
               {
                   result = res,
                   msg = msg
               }, JsonRequestBehavior.AllowGet);
        }
    }
}


 