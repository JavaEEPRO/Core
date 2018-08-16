using arkAS.Areas.Shtoda.BLL;
using arkAS.BLL;
using arkAS.BLL.Core;
using arkAS.Models;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace arkAS.Areas.Shtoda.Controllers
{
    public class ContractsController : BaseController
    {
        public ContractsController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        // GET: Shtoda/Contracts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contract()
        {
            ViewBag.ListType = mng.Contracts.GetDocTypes();
            ViewBag.ListTemplate = mng.Contracts.GetDocTypeTemplates();
            ViewBag.ListContagent = mng.Contracts. GetContagents();
            ViewBag.ListStatuses = mng.Contracts.GetStatusesContract();
            return View();
        }

        public ActionResult Contract_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);

            var number = "";
            var date = "";
            var contagentID = "";
            var total = "";
            var description = "";
            var status = "";
            var parent = "";
            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                date = parameters.filter.ContainsKey("date") ? parameters.filter["date"].ToString() : "";
                status = parameters.filter.ContainsKey("status") ? parameters.filter["status"].ToString() : "0";
                contagentID = parameters.filter.ContainsKey("contagentID") ? parameters.filter["contagentID"].ToString() : "0";

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

            p.Add("number", number);
            p.Add("contagentID", contagentID);
            p.Add("status", status);
            p.Add("sort1", sort1);
            p.Add("direction1", direction1);
            p.Add("createdMin", createdMin);
            p.Add("createdMax", createdMax);
            p.Add("page", parameters.page);
            p.Add("pageSize", parameters.pageSize);
            p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var items = rep.GetSQLData<dynamic>("GetContractsShtoda", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

            var total = p.Get<int>("total");

            var json = JsonConvert.SerializeObject(new
            {
                items,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult CreateContract(string number, string path, int typeID, int contagentID, string description, decimal total)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
            int status = 1;
            var item = new Shtoda_contracts
            {
                id = 0,
                number = number,
                date = DateTime.Now.Date,
                path = path,
                typeID = typeID,
                status = status,
                contagentID = contagentID,
                total = total,
                description = description
            };
            mng.Contracts.SaveContract(item);

            return Json(new
            {
                result = item.id > 0,
                savedID = item.id,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContract(int id)
        {

            if (mng.Contracts.IsCanCurrentUserChange())
            {
                var item = mng.Contracts.GetContract(id);
                item.path = CopyFileOnFolderID(item.id, item.path);

                return Json(new
                {
                    id = item.id,
                    number = item.number,
                    path = item.path,
                    typeID = item.typeID,
                    status = item.status,
                    contagentID = item.contagentID,
                    date = item.date.ToString("dd.MM.yyyy"),
                    total = item.total,
                    description = item.description,
                    msg = ""
                });
            }
            else
            {
                return Json(new
                {
                    msg = "У вас нет прав для редактирования"
                });
            }
            
        }

        public ActionResult Contract_remove(string id)
        {
            var parameters = AjaxModel.GetAjaxParameters(HttpContext);
           
            var res = false;
            var msg = "";
            var user = mng.GetUser();
                res = mng.Contracts.DeleteContract(user,int.Parse(id), out msg);

                return Json(new
                {
                    result = res,
                    msg = msg
                }, JsonRequestBehavior.AllowGet);
            }

        public ActionResult EditContract(Shtoda_contracts item)
        {

            mng.Contracts.SaveContract(item);
            return Json(new { result = true });
        }

        public ActionResult ContractInline(int pk, string value, string name)
        {
            string msg = "";
            var user = mng.GetUser();
            
            var res = mng.Contracts.EditContract(user,pk, name, value, out msg);

            return Json(new
            {
                result = res,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }

        public string CopyFileOnFolderID(int id, string path)
        {
            string dirPathTempate = Server.MapPath(path);

            string fileName = Path.GetFileName(path);
            string folderName = "ID" + id + "/";
            string pathFolder = "/Areas/Shtoda/uploads/Contracts/";
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
                else
                {
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }

            return res;
        }

        public ActionResult DownloadDoc(int docId, string path)
        {
            string fullPath = path;

            return Json(new
            {
                result = fullPath

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadDoc(int docId, string note)
        {
            string directoryPath = Server.MapPath("~/Areas/Shtoda/uploads/Contracts/") + "ID" + docId + "/";
            var res = false;
            var fileName = "";
            var exMessage = "";

            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    var file = files[0];

                    fileName = UploadContract(file, directoryPath);
                    string namePath = directoryPath + fileName;
                    file.SaveAs(namePath);

                    fileName = "/Areas/Shtoda/uploads/Contracts/ID" + docId + "/" + fileName;

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

        public string UploadContract(HttpPostedFileBase file, string directoryPath)
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


        public ActionResult GetListTemplatesByTypeId(int typeId)
        {
            var items = mng.Contracts.GetListTemplatesByType(typeId);
            return Json(new
            {
                result = items.Select(x => new
                {
                    id = x.id,
                    path = x.path,
                    name = x.name,
                    typeId = x.typeID

                }
                    )
            });
        }
    }
}

