using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Molchanov.BLL;
using Newtonsoft.Json;
using arkAS.BLL;
using arkAS.Models;
using arkAS.Areas.Molchanov.Models;
using System.Collections;

namespace arkAS.Areas.Molchanov.Controllers
{
    [Authorize(Roles = "Admin, Guest")]
    public class DocumentController : BaseController
    {
        public DocumentController(IManager mng) : base(mng)
        {

        }
        #region Documents
        public ActionResult DocumentsList()
        {
            var user = mng.GetUser();
            string msg = "";
            var model = new DocumentViewModel
            {
                Contragent = mng.Contragents.GetContragents(out msg, user),
                DocStatuses = mng.Documents.GetDocStatuses(out msg, user),
                DocTypes = mng.Documents.GetDocTypes(out msg, user),
                Documents = mng.Documents.GetDocuments(out msg, user)
            };
            return View(model);
        }
        public ActionResult DocumentsList_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg = "";
            var items = mng.Documents.GetDocuments(out msg, user);
            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var text = parameters.filter.ContainsKey("text") ? parameters.filter["text"].ToString() : "";
                if (text != "")
                {
                    items = items.Where(x => x.number != null && x.number.Contains(text)).ToList();
                }
                var ctrgID = parameters.filter.ContainsKey("ctrgID") ? RDL.Convert.StrToInt(parameters.filter["ctrgID"].ToString(), 0) : 0;
                if (ctrgID != 0)
                {
                    items = items.Where(x => x.contragentID == ctrgID).ToList();
                }
                var docTypeID = parameters.filter.ContainsKey("docTypeID") ? RDL.Convert.StrToInt(parameters.filter["docTypeID"].ToString(), 0) : 0;
                if (docTypeID != 0)
                {
                    items = items.Where(x => x.docTypeID == docTypeID).ToList();
                }
                var parentDocID = parameters.filter.ContainsKey("parentDocID") ? RDL.Convert.StrToInt(parameters.filter["parentDocID"].ToString(), 0) : 0;
                if (parentDocID != 0)
                {
                    items = items.Where(x => x.docParentID == parentDocID).ToList();
                }
                List<int?> statusIDs = new List<int?>();
                if (parameters.filter.ContainsKey("statusIDs"))
                {
                    statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                }
                if (statusIDs.Count != 0)
                {
                    items = items.Where(x => statusIDs.Contains(x.docStatusID)).ToList();
                }
            }
            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            switch (sort1)
            {
                case "number":
                    if (direction1 == "up") items = items.OrderBy(x => x.number).ToList();
                    else items = items.OrderByDescending(x => x.number).ToList();
                    break;
                case "email":
                    if (direction1 == "up") items = items.OrderBy(x => x.date).ToList();
                    else items = items.OrderByDescending(x => x.date).ToList();
                    break;
                case "sum":
                    if (direction1 == "up") items = items.OrderBy(x => x.sum).ToList();
                    else items = items.OrderByDescending(x => x.sum).ToList();
                    break;
                default:
                    if (direction1 == "up") items = items.OrderBy(x => x.number).ToList();
                    else items = items.OrderByDescending(x => x.number).ToList();
                    break;
            }
            var total = items.Count();
            var res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new
                {
                    id = x.id,
                    uniqueCode = x.uniqueCode,
                    date = x.date.ToShortDateString(),
                    number = x.number,
                    sum = x.sum,
                    description = x.description,
                    link = x.link,
                    isDeleted = x.isDeleted,
                    contragentName = x.molchanov_contragents.name,
                    docStatus = x.molchanov_docStatuses.name,
                    docTypes = x.molchanov_docTypes.name,
                    ParentDocs = x.molchanov_documents2.number
                }),
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }
        public ActionResult DocumentsList_save()
        {
            var parameters = CRUDToDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            string msg = "";
            molchanov_documents item;
            int currentID = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);
            if (currentID == 0)
            {
                item = mng.Documents.CreateDocument(parameters, out msg, user);
            }
            else
            {
                item = mng.Documents.EditDocument(parameters, currentID, out msg, user);
            }
            var json = JsonConvert.SerializeObject(new { result = true, msg });
            return Content(json, "application/json");
        }
        public ActionResult DocumentsList_remove(int id)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            result = mng.Documents.RemoveDocument(id, out msg, user);
            if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result});
            }
        }
        public ActionResult Documents_changeInline(int pk, string value, string name)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            
            result = mng.Documents.ChangeDocumentInline(pk, name, value, out msg, user);
             if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result});
            }
        }
        public ActionResult GetDocumentLogs(int id)
        {
            var res = mng.Documents.GetDocLogsByID(id);
            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new
                {
                   date =  x.date.ToShortDateString(),
                   notice = x.notice,
                   userName = x.userName
                })
            });
            return Content(json, "application/json");
        }
        #endregion
        #region DocStatuses
        public ActionResult DocStatusesList()
        {
            return View();
        }
        public ActionResult DocStatusesList_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg = "";
            var items = mng.Documents.GetDocStatuses(out msg, user);
            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var text = parameters.filter.ContainsKey("text") ? parameters.filter["text"].ToString() : "";
                if (text != "")
                {
                    items = items.Where(x => x.name != null && x.name.Contains(text)).ToList();
                }
            }
            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            switch (sort1)
            {
                case "name":
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
                case "email":
                    if (direction1 == "up") items = items.OrderBy(x => x.code).ToList();
                    else items = items.OrderByDescending(x => x.code).ToList();
                    break;
                default:
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
            }
            var total = items.Count();
            var res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new
                {
                    id = x.id,
                    name = x.name,
                    code = x.code
                }),
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }
        public ActionResult DocStatusesList_save()
        {
            var parameters = CRUDToDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            string msg = "";
            molchanov_docStatuses item;
            int currentID = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);
            if (currentID == 0)
            {
                item = mng.Documents.CreateDocStatus(parameters, out msg, user);
            }
            else
            {
                item = mng.Documents.EditDocStatus(parameters, currentID, out msg, user);
            }
            var json = JsonConvert.SerializeObject(new { result = true, msg });
            return Content(json, "application/json");
        }
        public ActionResult DocStatusesList_remove(int id)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            result = mng.Documents.RemoveDocStatus(id, out msg, user);
            if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result });
            }
        }
        #endregion
        #region DocTypes
        public ActionResult DocTypesList()
        {
            return View();
        }
        public ActionResult DocTypesList_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg = "";
            var items = mng.Documents.GetDocTypes(out msg, user);
            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var text = parameters.filter.ContainsKey("text") ? parameters.filter["text"].ToString() : "";
                if (text != "")
                {
                    items = items.Where(x => x.name != null && x.name.Contains(text)).ToList();
                }
            }
            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            switch (sort1)
            {
                case "name":
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
                case "email":
                    if (direction1 == "up") items = items.OrderBy(x => x.code).ToList();
                    else items = items.OrderByDescending(x => x.code).ToList();
                    break;
                default:
                    if (direction1 == "up") items = items.OrderBy(x => x.name).ToList();
                    else items = items.OrderByDescending(x => x.name).ToList();
                    break;
            }
            var total = items.Count();
            var res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                items = res.Select(x => new
                {
                    id = x.id,
                    name = x.name,
                    code = x.code
                }),
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }
        public ActionResult DocTypesList_save()
        {
            var parameters = CRUDToDictionary(AjaxModel.GetAjaxParameters(HttpContext));
            var user = mng.GetUser();
            string msg = "";
            molchanov_docTypes item;
            int currentID = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);
            if (currentID == 0)
            {
                item = mng.Documents.CreateDocType(parameters, out msg, user);
            }
            else
            {
                item = mng.Documents.EditDocType(parameters, currentID, out msg, user);
            }
            var json = JsonConvert.SerializeObject(new { result = true, msg });
            return Content(json, "application/json");
        }

        public ActionResult DocTypesList_remove(int id)
        {
            var user = mng.GetUser();
            string msg = "";
            var result = false;
            result = mng.Documents.RemoveDocType(id, out msg, user);
            if (!result)
            {
                return Json(new { result = result, msg = msg });
            }
            else
            {
                return Json(new { result = result });
            }
        }
        #endregion
    }
}