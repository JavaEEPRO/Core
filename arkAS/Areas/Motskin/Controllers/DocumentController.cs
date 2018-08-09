using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Motskin.BLL;
using arkAS.Areas.Motskin.Models;
using arkAS.Models;
using Newtonsoft.Json;
using System.IO;

namespace arkAS.Areas.Motskin.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly string baseUrl = "~/Areas/Motskin/uploads/Documents/ID";
        public DocumentController(IManager mng) : base(mng)
        {
            
        }

        public ActionResult Documents()
        {
            DocumentViewModel model = new DocumentViewModel();
            model.Types = mng.Documents.GetTypes();
            model.Contractors = mng.Contractors.GetAll();
            model.Statuses = mng.Documents.GetStatuses();
            return View(model);
        }
        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg;
            var total = 0;
            var items = mng.Documents.GetItems(parameters, user, out msg, out total);
            var json = JsonConvert.SerializeObject(new
            {
                items,
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }

        public ActionResult GetDocument(int id)
        {
            var user = mng.GetUser();
            string msg;
            var item = mng.Documents.Get(id,user,out msg);
            return Json(new
            {
                id = item.id,
                number = item.number,
                reference = item.reference,
                documentTypeID = item.documentTypeID,
                documentStatusID = item.documentStatusID,
                contractorID = item.contractorID,
                date = item.date.ToString("dd.MM.yyyy"),
                sum = item.sum,
                comment = item.comment
            });
        }

        public ActionResult ChangeDocumentStatus(int pk, string value, string name)
        {
            string msg;
            var result = false;
            var user = mng.GetUser();
            int newStatus = int.Parse(value);
            result = mng.Documents.ChangeStatus(pk, value, name, user, out msg);
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

        public Dictionary<string,string> ToSimpleDictionary(NameValueCollection list)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach(var key in list.AllKeys)
            {
                var value = list[key].ToString();
                res.Add(key, value);
            }
            return res;
        }

        public ActionResult Documents_save()
        {
            string msg;
            var parameters = ToSimpleDictionary(HttpContext.Request.Form);

            var user = mng.GetUser();
            var item = mng.Documents.Create(parameters, user, out msg);
            if (msg == "")
            {
                if (!uploadFile(item.id))  // после создания документа у нас есть id и мы можем для него загрузить документы.
                {
                    string message;
                    mng.Documents.Delete(item.id, user, out message); // если произошла ошибка, то нужно откатить добавление документа.
                    msg = "Ошибка при загрузке файла";
                }
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

        public ActionResult Documents_edit()
        {
            string msg="";
            var parameters = ToSimpleDictionary(HttpContext.Request.Form);

            var user = mng.GetUser();
            int id;
            if (!int.TryParse(parameters["id"].ToString(), out id))
            {
                msg = "Не верный формат идентификатора";
            }

            if (msg == "") // если всё в порядке
            {
                if (!uploadFile(id,false)) 
                {
                    msg = "Ошибка при загрузке файла";
                }
            }
            if (msg == "") // если всё в порядке
            {
                var item = mng.Documents.Edit(parameters, user, out msg);
                if (msg == "") // если всё в порядке
                {
                    return Json(new { result = true, itemID = item.id });
                }
            }

            return Json(new
            {
                result = false,
                msg = msg
            });
        }

        public ActionResult Documents_remove(int id)
        {
            var result = false;
            var user = mng.GetUser();
            string msg;
            result = mng.Documents.Delete(id, user, out msg);

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

        public ActionResult LogDocumentStatuses(int id)
        {
            var items = mng.Documents.GetLogStatus(id);
            var json = JsonConvert.SerializeObject(new
            {
                items = items.OrderBy(x => x.created).Select(x => new
                {
                    status = x.motskin_documentStatuses.name,
                    created = x.created.ToString("dd.MM.yyyy"),
                    x.note
                })
            });
            return Content(json, "application/json");
        }

        private bool uploadFile(int docId, bool isRequired=true)
        {
            string directoryPath = Server.MapPath(baseUrl + docId + "/");
            var res = false;

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                HttpFileCollectionBase files = Request.Files;
                if (files.Count > 0)
                {
                    var file = files[0];

                    string namePath = directoryPath + file.FileName;
                    FileInfo fi = new FileInfo(namePath);
                    if (fi.Exists)   // если файл существует, то удаляем его
                        fi.Delete();
                    file.SaveAs(namePath);
                    res = true;
                }
                else if (!isRequired)  // а если загрузка файла не обязательна (при редактировании), то
                {
                    res = true;     // считаем, что всё в порядке
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }

        public ActionResult DownloadDoc(int id)
        {
            var user = mng.GetUser();
            string msg;
            string fileName = mng.Documents.Get(id, user, out msg).reference;
            string fullPath = Server.MapPath(baseUrl + id + "/"+fileName);
            byte[] fileBytes = null;

            try
            {
                fileBytes = System.IO.File.ReadAllBytes(fullPath);
            }
            catch (FileNotFoundException ex)
            {
                RDL.Debug.LogError(ex,"отсутствует файл "+fullPath);
            }
            catch(Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            if (fileBytes != null)
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            else
                return View("_NotFoundedFile");

        }
    }
}