using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.BLL;
using arkAS.BLL.Core;
using arkAS.Areas.Rosh.BLL;
using arkAS.Areas.Rosh.Models;
using arkAS.Models;
using arkAS.BLL.HR;
using arkAS.BLL.Finance;
using System.Data;
using Newtonsoft.Json;
using Dapper;
using System.Collections;
using System.Web.Security;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web.Http.ModelBinding;
using DocumentFormat.OpenXml.Spreadsheet;

namespace arkAS.Areas.Rosh.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class DocumentsController: BaseController
    {
        public DocumentsController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        #region Documents

        public ActionResult Documents()
        {
            DocumentsViewModel model = new DocumentsViewModel();
            model.Documents = mng.Documents.GetDocuments();
            model.DocTypes = mng.Documents.GetDocTypes();
            model.DocStatuses = mng.Documents.GetDocStatuses();
            model.Contragents = mng.Contragents.GetContragents();
            return View(model);
        }

        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);

            var docDate = "";
            var docTypeID = "";
            var docNumber = "";
            var contragentID = "";
            var docStatusID = "";
            var amount = "";
            var description = "";
            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                docTypeID = parameters.filter.ContainsKey("docTypeID") ? parameters.filter["docTypeID"].ToString() : "";
                docNumber = parameters.filter.ContainsKey("docNumber") ? parameters.filter["docNumber"].ToString() : "";
                contragentID = parameters.filter.ContainsKey("contragentID") ? parameters.filter["contragentID"].ToString() : "0";
                docStatusID = parameters.filter.ContainsKey("docStatusID") ? parameters.filter["docStatusID"].ToString() : "0";

                if (parameters.filter.ContainsKey("docDate") && parameters.filter["docDate"] != null)
                {
                    var dates = parameters.filter["docDate"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
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
            p.Add("docDate", docDate);
            p.Add("docTypeID", docTypeID);
            p.Add("docNumber", docNumber);
            p.Add("contragentID", contragentID);
            p.Add("docStatusID", docStatusID);
            p.Add("amount", amount);
            p.Add("description", description);
            p.Add("createdMin", createdMin);
            p.Add("createdMax", createdMax);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("Rosh_GetDocuments", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult Documents_createItem(int docTypeID, string docNumber, int contragentID, 
                                                 int docStatusID, string amount, string description)
        {
            string msg = "";
            var user = mng.GetUser();

            var item = new rosh_documents
            {
                id = 0,
                docDate = DateTime.Now.Date,
                docTypeID = docTypeID,
                docNumber = docNumber,
                contragentID = contragentID,
                docStatusID = docStatusID,
                amount = Convert.ToDecimal(amount),
                description = description,
            };
            mng.Documents.SaveDocument(item, user, out msg);
            return Json(new
            {
                result = item.id > 0,
                savedID = item.id,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Documents_GetItem(int id)
        {
            var res = false;

            if (Roles.IsUserInRole("admin"))
            {
                var item = mng.Documents.GetDocument(id);

                return Json(new
                {
                    res = true,
                    id = item.id,
                    docDate = item.docDate.ToString("dd.MM.yyyy"),
                    docTypeID = item.docTypeID,
                    docTypeName = item.rosh_docTypes.name,
                    docNumber = item.docNumber,
                    contragentID = item.contragentID,
                    contragentName = item.rosh_contragents.name,
                    docStatusID = item.docStatusID,
                    docStatusName = item.rosh_docStatuses.name,
                    amount = item.amount.ToString(),
                    description = item.description,
                });
            }
            else
            {
                return Json(new { result = res, msg = "Недостаточно прав" }, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "admin")]
        public ActionResult Documents_editItem(int id, string docDate, int docTypeID, string docNumber, int contragentID,
                                                 int docStatusID, string amount, string description)
        {
            string msg = "";
            var user = mng.GetUser();

            var item = new rosh_documents
            {
                id = id,
                docDate = DateTime.Parse(docDate),
                docTypeID = docTypeID,
                docNumber = docNumber,
                contragentID = contragentID,
                docStatusID = docStatusID,
                amount = Convert.ToDecimal(amount),
                description = description,
            };

            mng.Documents.SaveDocument(item, user, out msg);

            return Json(new { result = true, savedID = item.id, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        //[Authorize(Roles = "admin")]
        public ActionResult Documents_deleteItem(int id)
        {
            string msg = "";
            var user = mng.GetUser();
            var res = false;

            if (id != 0)
            {
                mng.Documents.DeleteDocument(id, user, out msg);
                res = true;
            }

            return Json(new
            {
                result = res,
                msg
            });
        }

        public ActionResult DocumentsInline(int pk, string value, string name)
        {
            mng.Documents.EditDocField(pk, name, value);
            return Json(new
            {
                result = true
            });
        }

        #endregion

        #region DocTypes

        public ActionResult DocTypes()
        {
            //DocumentsViewModel model = new DocumentsViewModel();
            //model.DocTypes = mng.Documents.GetDocTypes();
            //return View(model);
            return View();
        }

        public ActionResult DocTypes_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var name = "";
            var parentID = "";

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var rep = new CoreRepository();
            var p = new DynamicParameters();
            p.Add("name", name);
            p.Add("parentID", parentID);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("Rosh_GetDocTypes", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult DocTypes_createItem()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);

            var res = false;
            int savedID = 0;
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();

                var id = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0);
                var name = AjaxModel.GetValueFromSaveField("name", fields);
                var parentID = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("parentID", fields), 0);

                var item = new rosh_docTypes { id = id, name = name, parentID = parentID };
                mng.Documents.SaveDocType(item);
                savedID = item.id;
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
               // _debug(ex, new { id }, "");
            }
            return Json(new
            {
                result = res,
                savedID = savedID,
                msg = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocTypes_deleteItem(int id)
        {
            var res = false;

            var item = mng.Documents.GetDocType(id);
            if (item != null)
            {
                mng.Documents.DeleteDocType(id);
                res = true;
            }

            return Json(new
            {
                result = res,
                msg = "Тип удален!"
            });
        }
        #endregion

        #region DocStatuses

        public ActionResult DocStatuses()
        {
            DocumentsViewModel model = new DocumentsViewModel();
            model.DocStatuses = mng.Documents.GetDocStatuses();
            return View(model);
        }

        public ActionResult DocStatuses_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var name = "";
            var code = "";
            var color = "";

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var rep = new CoreRepository();
            var p = new DynamicParameters();
            p.Add("name", name);
            p.Add("code", code);
            p.Add("color", color);

            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("Rosh_GetDocStatuses", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult DocStatuses_createItem()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);

            var res = false;
            int savedID = 0;
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();

                var id = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0);
                var name = AjaxModel.GetValueFromSaveField("name", fields);
                var code = AjaxModel.GetValueFromSaveField("code", fields);
                var color = AjaxModel.GetValueFromSaveField("color", fields);

                var item = new rosh_docStatuses { id = id, name = name, code = code, color = color };
                mng.Documents.SaveDocStatus(item);
                savedID = item.id;
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                // _debug(ex, new { id }, "");
            }
            return Json(new
            {
                result = res,
                savedID = savedID,
                msg = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocStatuses_deleteItem(int id)
        {
            var res = false;

            var item = mng.Documents.GetDocStatus(id);
            if (item != null)
            {
                mng.Documents.DeleteDocStatus(id);
                res = true;
            }

            return Json(new
            {
                result = res,
                msg = "Статус удален!"
            });
        }

        #endregion

        #region DocLogs

        public ActionResult DocLogs()
        {
            DocumentsViewModel model = new DocumentsViewModel();
            model.Documents = mng.Documents.GetDocuments();
            model.DocTypes = mng.Documents.GetDocTypes();
            return View(model);
        }

        public ActionResult DocLogs_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);

            var date = "";
            var documentID = "";
            var changes = "";
            var docNumber = "";

            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                docNumber = parameters.filter.ContainsKey("docNumber") ? parameters.filter["docNumber"].ToString() : "";

                if (parameters.filter.ContainsKey("date") && parameters.filter["date"] != null)
                {
                    var dates = parameters.filter["date"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
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
            p.Add("date", date);
            p.Add("documentID", documentID);
            p.Add("docNumber", docNumber);
            p.Add("changes", changes);

            p.Add("createdMin", createdMin);
            p.Add("createdMax", createdMax);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("Rosh_GetDocLogs", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult DocLogs_deleteItem(int id)
        {
            var res = false;

            var item = mng.Documents.GetDocLog(id);
            if (item != null)
            {
                mng.Documents.DeleteDocLog(id);
                res = true;
            }

            return Json(new
            {
                result = res,
                msg = "Лог удален!"
            });
        }

        #endregion

    }
}