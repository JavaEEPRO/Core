using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;
using arkAS.BLL;
using arkAS.Models;
using System.Data;
using arkAS.BLL.Core;
using Newtonsoft.Json;
using Dapper;
using System.IO;

namespace arkAS.Areas.Podvashetskyi.Controllers
{
    public class DocumentsController : BaseController
    {
        public DocumentsController()
            : base(new Manager(new PdvDocumentRepository(new LocalSqlServer())))
        {

        }
        public ActionResult Index()
        {
            return View();
        }
        // Documents 
        public ActionResult Documents()
        {
            ViewBag.DocTypes = mng.Documents.GetDocumentTypes();
            ViewBag.DocStatuses = mng.Documents.GetDocumentStatuses();
            ViewBag.Contragents = mng.Contractors.GetContractors();
            return View();
        }
        public ActionResult CreateDocument(string name, string number, string link, int documentTypeID, int contractorID)
        {
            int statusID = mng.Documents.GetDocumentStatusCodeId("CREATED");
            var item = new pdv_documents
            {
                id = 0,
                name = name,
                number = number,
                documentTypeID = documentTypeID,
                documentStatusID = documentTypeID,
                contractorID = contractorID,
                createdDate = DateTime.Now.Date,
                createdBy = new CoreManager().GetUserGuid(),
                documentID = null
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
            var link = "";
            var number = "";
            var documentTypeID = "";
            var documentStatusID = "";
            var contractorID = "";

            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                name = parameters.filter.ContainsKey("name") ? parameters.filter["name"].ToString() : "";
                number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                link = parameters.filter.ContainsKey("link") ? parameters.filter["link"].ToString() : "";
                documentTypeID = parameters.filter.ContainsKey("documentTypeID") ? parameters.filter["documentTypeID"].ToString() : "0";
                documentStatusID = parameters.filter.ContainsKey("documentStatusID") ? parameters.filter["documentStatusID"].ToString() : "0";
                contractorID = parameters.filter.ContainsKey("contractorID") ? parameters.filter["contractorID"].ToString() : "0";

                if (parameters.filter.ContainsKey("createdDate") && parameters.filter["createdDate"] != null)
                {
                    var dates = parameters.filter["createdDate"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
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
            p.Add("link", link);
            p.Add("documentTypeID", documentTypeID);
            p.Add("documentStatusID", documentStatusID);
            p.Add("contractorID", contractorID);
            p.Add("createdMin", createdMin);
            p.Add("createdMax", createdMax);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("PDV_GetDocs", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

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
            var user = mng.GetUser();
            mng.Documents.EditDocumentField(pk, name, value, user, out msg);
            return Json(new
            {
                result = true
            });
        }
        public ActionResult GetDocument(int id)
        {
            var item = mng.Documents.GetDocument(id);
            item.link = CopyFileOnFolderID(item.id, item.link);
            return Json(new
            {
                id = item.id,
                name = item.name,
                number = item.number,
                link = item.link,
                documentTypeID = item.documentTypeID,
                documentStatusID = item.documentStatusID,
                contractorID = item.contractorID,
                createdDate = item.createdDate.ToString("dd.MM.yyyy"),
                sum = item.sum,
                comment = item.comment
            });
        }
        public ActionResult EditDocument(pdv_documents item)
        {
            mng.Documents.SaveDocument(item);
            return Json(new
            {
                result = true
            });
        }
        //public ActionResult DownloadDocument(int documentId, string link)
        //{
        //    string fullPath = link;
        //    return Json(new
        //    {
        //        result = fullPath
        //    });
        //}
        [HttpPost]
        public ActionResult UploadDocuments(int docId, string note)
        {
            string directoryPath = Server.MapPath("~/uploads/PdvDocuments/") + "ID" + docId + "/";
            var res = false;
            var fileName = "";
            var exMessage = "";

            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    var file = files[0];

                    fileName = UploadDocument(file, directoryPath);
                    string namePath = directoryPath + fileName;
                    file.SaveAs(namePath);

                    fileName = "/uploads/PdvDocuments/ID" + docId + "/" + fileName;

                    res = true;
                }
            }
            catch (Exception ex)
            {
                exMessage = ex.Message;
                RDL.Debug.LogError(ex);
            }

            return Json(new
            {
                result = res,
                fileName = fileName,
                exMessage = exMessage

            }, JsonRequestBehavior.AllowGet);
        }
        // стоит вынести в BLL
        public string CopyFileOnFolderID(int id, string link)
        {
            string dirPathTempate = Server.MapPath(link);
            string fileName = Path.GetFileName(link);
            string folderName = "ID" + id + "/";
            string pathFolder = "/uploads/PdvDocuments/";
            string pathDoc = pathFolder + folderName;
            string dirPathDoc = Server.MapPath(pathDoc) + fileName;
            var res = pathDoc + fileName;
            try
            {
                if (!System.IO.File.Exists(dirPathDoc))
                {
                    if (!Directory.Exists(Server.MapPath(pathDoc)))
                    {
                        Directory.CreateDirectory(Server.MapPath(pathDoc));
                    }
                    System.IO.File.Copy(dirPathTempate, dirPathDoc, true);
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        public string UploadDocument(HttpPostedFileBase file, string directoryPath)
        {
            string finalFileName = "";
            int counter = 0;

            if (file != null && file.ContentLength > 0)
            {
                for (; ; )
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(fileName);
                    fileName = Path.GetFileNameWithoutExtension(fileName);

                    finalFileName = fileName + "_" + ((counter).ToString()) + extension;
                    string namePath = directoryPath + finalFileName;

                    if (System.IO.File.Exists(namePath))
                    {
                        ++counter;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return finalFileName;
        }

        // Doc Types
        public ActionResult DocTypes()
        {
            return View();
        }
        public ActionResult DocTypes_getItems()
        {
            var items = mng.Documents.GetDocumentTypes();
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
        public ActionResult DocTypes_save()
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newDocType = new pdv_documentType
                {
                    id = (AjaxModel.GetValueFromSaveField("id", fields) == "") ? 0 : int.Parse(AjaxModel.GetValueFromSaveField("id", fields)),
                    name = AjaxModel.GetValueFromSaveField("name", fields),
                    code = AjaxModel.GetValueFromSaveField("code", fields)
                };

                mng.Documents.SaveDocumentTypes(newDocType);
                return Json(new
                {
                    result = true,
                    id = newDocType.id,
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
        public ActionResult DocTypes_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Documents.DeleteDocumentTypes(int.Parse(id));
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
        public ActionResult DocTypesInline(int pk, string value, string name)
        {
            var msg = "";
            var res = mng.Documents.EditDocumentTypesField(pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
        // DocStatuses
        public ActionResult DocStatuses()
        {
            return View();
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
            try
            {
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                var newDocType = new pdv_documentStatuses
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
        public ActionResult DocStatuses_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            var res = false;
            var msg = "";
            try
            {
                res = mng.Documents.DeleteDocumentStatus(int.Parse(id));
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