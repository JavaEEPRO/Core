using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.BLL;
using arkAS.Areas.Udovika.BLL;
using arkAS.Models;
using arkAS.Areas.Udovika.Models;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using arkAS.BLL.Core;
using Dapper;

namespace arkAS.Areas.Udovika.Controllers
{
    public class DocumentsController : BaseController
    {
        public DocumentsController() : base(new Manager(new Repository(new LocalSqlServer()))) { }
        #region JSRender
        public ActionResult Render()
        {
            return View();
        }
        public JsonResult GetContractors()
        {
            var contractors = mng.Contractor.GetContractors();
            return Json(contractors.Select(x => new
            {
                id = x.id,
                name = x.name,
                
            }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetContractor(int id)
        {
            var item = mng.Contractor.GetContractor(id);
            if (item != null)
            {
                return Json(new
                {
                    id = item.id,
                    name = item.name,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    msg = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult docum()
        {


            var parameters = AjaxModel.GetParameters(HttpContext);
            var items = mng.Document.GetDocuments();
            int typeID = 0;
            int contractorID = 0;

            var total = items.Count();
            var res2 = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();

            //return Json(new
            //{
            //    items = res2.Select(x => new
            //    {
            //        x.id,
            //        date = x.date.ToString("dd.MM.yyyy"),
            //        //x.contractorID,
            //        //contractor = x.udovika_contractor != null ? x.udovika_contractor.name : "",
            //        //x.statusID,
            //        //status = x.udovika_statusContract != null ? x.udovika_statusContract.name : "",
            //        //x.typeID,
            //        //type = x.udovika_contractType != null ? x.udovika_contractType.name : "",
            //        x.link,
            //        x.number,
            //        //x.total,
            //        //x.comment
            //    }),
            //    total = total
            //}, JsonRequestBehavior.AllowGet);
            return Json(items.Select(x => new {
                x.id,
                date = x.date.ToString("dd.MM.yyyy"),
                x.number,
                x.link,
                contractor = x.udovika_contractor != null ? x.udovika_contractor.name : "",
                x.contractorID,
                x.total,
                x.typeID,
                type = x.udovika_contractType != null ? x.udovika_contractType.name : "",
                x.statusID,
                x.comment
            }), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Documents
        // GET: Udovika/Documents
        public ActionResult Index()
        {
            DocumentView source = new DocumentView();
            source.contractors = mng.Contractor.GetContractors();
            source.contractStatuses = mng.Document.GetDocumentStatuses();
            source.contractTypes = mng.Document.GetDocumentTypes();
            
            return View(source);
        }
        public ActionResult Documents_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var items = mng.Document.GetDocuments().AsQueryable();
            int typeID = 0;
            int contractorID = 0;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                if (!String.IsNullOrEmpty(number))
                {
                    items = items.Where(x => x.number == number);
                }

                typeID = parameters.filter.ContainsKey("typeID") ? RDL.Convert.StrToInt(parameters.filter["typeID"].ToString(), 0) : 0;
                if (typeID > 0)
                {
                    items = items.Where(x => x.typeID == typeID);
                }

                List<int?> statusIDs = new List<int?>();
                if (parameters.filter.ContainsKey("statusIDs"))
                {
                    statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    //var statusIDs =  RDL.Convert.StrToInt(parameters.filter["status"].ToString(), 0);
                }
                items = items.Where(x =>
                    (statusIDs.Count == 0 || statusIDs.Contains(x.statusID))
                );

                contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                if (contractorID > 0)
                {
                    items = items.Where(x => x.contractorID == contractorID);
                }
            }

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var sort2 = sorts.Length > 1 ? sorts[1] : "";
            var direction2 = directions.Length > 1 ? directions[1] : "";


            switch (sort1)
            {
                case "number":
                    if (direction1 == "up") items = items.OrderBy(x => x.number);
                    else items = items.OrderByDescending(x => x.number);
                    break;
                case "date":
                    if (direction1 == "up") items = items.OrderBy(x => x.date);
                    else items = items.OrderByDescending(x => x.date);
                    break;
                case "type":
                    if (direction1 == "up") items = items.OrderBy(x => x.typeID);
                    else items = items.OrderByDescending(x => x.typeID);
                    break;
                case "contractor":
                    if (direction1 == "up") items = items.OrderBy(x => x.contractorID);
                    else items = items.OrderByDescending(x => x.contractorID);
                    break;
                default:
                    break;
            }


            var total = items.Count();
            var res2 = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();



            return Json(new
            {
                items = res2.Select(x => new
                {
                    x.id,
                    date = x.date.ToString("dd.MM.yyyy"),
                    x.contractorID,
                    contractor = x.udovika_contractor != null ? x.udovika_contractor.name : "",
                    x.statusID,
                    status = x.udovika_statusContract != null ? x.udovika_statusContract.name : "",
                    x.typeID,
                    type = x.udovika_contractType != null ? x.udovika_contractType.name : "",
                    x.link,
                    x.number,
                    x.total,
                    x.comment
                }),
                total = total
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DocumentsInlineEdit(int pk, string value, string name)
        {
            var result = false;
            string msg = "";
            var user = mng.GetUser();
            result = mng.Document.EditDocumentField(pk, name, value, out msg, user);
            if (!result)
            {
                return Json(new
                {
                    result = true,
                    msg = msg
                });
            }
            else
            {
                return Json(new
                {
                    result = result,
                });
            }
            
        }
        public ActionResult Documents_Create(string number, int tcontr, 
            int total, int typeDoc, string link, string comment)
        {
            var msg = "";
            var user = mng.GetUser();
            var item = new udovika_contract();
            item.id = 0;
            item.number = number;
            item.contractorID = tcontr;
            item.total = Convert.ToDecimal(total);
            item.typeID = typeDoc;
            item.link = link;
            item.comment = comment;
            item.statusID = 1;
            item.date = DateTime.Now.Date;

            var res = mng.Document.SaveDocument(item, user, out msg);
            
            return Json(new
            {
                result = item.id > 0,
                saveID = item.id,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDocument(int id)
        {
            var user = mng.GetUser();
            var msg = "";
            var item = mng.Document.GetDocument(id, user, out msg);
            if (item != null)
            {
                return Json(new
                {
                    id = item.id,
                    date = item.date.ToString("dd.MM.yyyy"),
                    number = item.number,
                    typeID = item.typeID,
                    contractorID = item.contractorID,
                    total = item.total,
                    comment = item.comment,
                    statusID = item.statusID,
                    link = item.link,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    msg = msg
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult EditDocument(udovika_contract item)
        {
            var user = mng.GetUser();
            var msg = "";
            var result = mng.Document.SaveDocument(item, user, out msg);
            if(result>=0)
            {
                return Json(new { result = result, msg = msg });
            }else
                return Json(new { result = result });
        }
        public ActionResult Documents_remove(int id)
        {
            var result = false;
            string msg = "";
            var user = mng.GetUser();
            result = mng.Document.DeleteDocument(id, user, out msg);
            if (!result)
            {
                return Json(new { 
                    result = result,
                    msg = msg
                });
            }else
                return Json(new { result = result });
        }
        #endregion
        #region Invoices
        public ActionResult Invoices()
        {
            InvoiceView source = new InvoiceView();
            source.invoices = mng.Invoice.GetInvoices();
            source.invoiceStatuses = mng.Invoice.GetStatuses();
            source.contractor = mng.Contractor.GetContractors();
            return View(source);
        }
        public ActionResult Invoices_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var items = mng.Invoice.GetInvoices().AsQueryable();
            int typeID = 0;
            int contractorID = 0;

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                if (!String.IsNullOrEmpty(number))
                {
                    items = items.Where(x => x.number == number);
                }

                //typeID = parameters.filter.ContainsKey("typeID") ? RDL.Convert.StrToInt(parameters.filter["typeID"].ToString(), 0) : 0;
                //if (typeID > 0)
                //{
                //    items = items.Where(x => x.typeID == typeID);
                //}

                List<int?> statusIDs = new List<int?>();
                if (parameters.filter.ContainsKey("statusIDs"))
                {
                    statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    //var statusIDs =  RDL.Convert.StrToInt(parameters.filter["status"].ToString(), 0);
                }
                items = items.Where(x =>
                    (statusIDs.Count == 0 || statusIDs.Contains(x.statusID))
                );

                contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                if (contractorID > 0)
                {
                    items = items.Where(x => x.contractorID == contractorID);
                }
            }

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var sort2 = sorts.Length > 1 ? sorts[1] : "";
            var direction2 = directions.Length > 1 ? directions[1] : "";


            switch (sort1)
            {
                case "number":
                    if (direction1 == "up") items = items.OrderBy(x => x.number);
                    else items = items.OrderByDescending(x => x.number);
                    break;
                case "date":
                    if (direction1 == "up") items = items.OrderBy(x => x.date);
                    else items = items.OrderByDescending(x => x.date);
                    break;
                //case "type":
                //    if (direction1 == "up") items = items.OrderBy(x => x.typeID);
                //    else items = items.OrderByDescending(x => x.typeID);
                //    break;
                case "contractor":
                    if (direction1 == "up") items = items.OrderBy(x => x.contractorID);
                    else items = items.OrderByDescending(x => x.contractorID);
                    break;
                default:
                    break;
            }


            var total = items.Count();
            var res2 = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();



            return Json(new
            {
                items = res2.Select(x => new
                {
                    x.id,
                    date = x.date.ToString("dd.MM.yyyy"),
                    x.contractorID,
                    contractor = x.udovika_contractor != null ? x.udovika_contractor.name : "",
                    x.statusID,
                    status = x.udovika_statusInvoice != null ? x.udovika_statusInvoice.name : "",
                    //x.typeID,
                    //type = x.udovika_contractType != null ? x.udovika_contractType.name : "",
                    //x.link,
                    number = x.number,
                    //x.total,
                    note = x.note
                }),
                total = total
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Invoice_Create(string number, int tcontr, string comment)
        {
            var msg = "";
            var user = mng.GetUser();
            var item = new udovika_invoice();
            item.id = 0;
            item.number = number;
            item.contractorID = tcontr;
            item.note = comment;
            item.statusID = 1;
            item.date = DateTime.Now.Date;

            var res = mng.Invoice.SaveInvoice(item, user, out msg);

            return Json(new
            {
                result = item.id > 0,
                saveID = item.id,
                msg = msg
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InvoiceInlineEdit(int pk, string value, string name)
        {
            string msg = "";
            var user = mng.GetUser();
            var res = mng.Invoice.EditInvoiceField(pk, name, value, out msg, user);
            if (!res)
            {
                return Json(new
                {
                    result = false,
                    msg = msg
                });
            }
            else
            {
                return Json(new
                {
                    result = true
                });
            }
        }
        public ActionResult GetInvoice(int id)
        {
            var msg = "";
            var user = mng.GetUser();
            var item = mng.Invoice.GetInvoice(id, user, out msg);
            if (item != null)
            {
                return Json(new
                {
                    id = item.id,
                    date = item.date.ToString("dd.MM.yyyy"),
                    number = item.number,
                    contractorID = item.contractorID,
                    comment = item.note,
                    statusID = item.statusID,
                });
            }
            else
            {
                return Json(new { msg = msg });
            }
        }
        public ActionResult EditInvoice(udovika_invoice invoice)
        {
            var msg = "";
            var user = mng.GetUser();
            var res = mng.Invoice.SaveInvoice(invoice, user, out msg);
            if (res > 0)
                return Json(new { result = true });
            else
                return Json(new { result = false, msg = msg });
            
        }
        public ActionResult Invoices_remove(int id)
        {
            string msg = "";
            var user = mng.GetUser();
            var result = mng.Invoice.DeleteInvoice(id, user, out msg);
            if (!result) return Json(new { result = result, msg = msg });
            else return Json(new { result = result });
        }
        #endregion
        #region Mails
        public ActionResult Mails()
        {
            MailsView source = new MailsView();
            source.mails = mng.Mail.GetMails();
            source.mailStatuses = mng.Mail.GetMailStatuses();
            source.contractor = mng.Contractor.GetContractors();
            return View(source);
        }
        public ActionResult Mails_getItems()
        {
            var parameters = AjaxModel.GetParameters(HttpContext);
            var items = mng.Mail.GetMails().AsQueryable();

            if (parameters.filter != null && parameters.filter.Count > 0)
            {
                var trackNum = parameters.filter.ContainsKey("trackNum") ? parameters.filter["trackNum"].ToString() : "";
                if (!String.IsNullOrEmpty(trackNum))
                {
                    items = items.Where(x => x.trackNum == trackNum);
                }

                var from = parameters.filter.ContainsKey("from") ? parameters.filter["from"].ToString() : "";
                if (!String.IsNullOrEmpty(from))
                {
                    items = items.Where(x => x.from == from);
                }

                var to = parameters.filter.ContainsKey("to") ? parameters.filter["to"].ToString() : "";
                if (!String.IsNullOrEmpty(to))
                {
                    items = items.Where(x => x.to == to);
                }

                List<int?> statusIDs = new List<int?>();
                if (parameters.filter.ContainsKey("statusIDs"))
                {
                    statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                }
                items = items.Where(x =>
                    (statusIDs.Count == 0 || statusIDs.Contains(x.statusID))
                );

                //var contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                //if (contractorID > 0)
                //{
                //    items = items.Where(x => x.contractorID == contractorID);
                //}
            }

            var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var sort1 = sorts.Length > 0 ? sorts[0] : "";
            var direction1 = directions.Length > 0 ? directions[0] : "";

            var sort2 = sorts.Length > 1 ? sorts[1] : "";
            var direction2 = directions.Length > 1 ? directions[1] : "";


            switch (sort1)
            {
                case "number":
                    if (direction1 == "up") items = items.OrderBy(x => x.trackNum);
                    else items = items.OrderByDescending(x => x.trackNum);
                    break;
                case "date":
                    if (direction1 == "up") items = items.OrderBy(x => x.date);
                    else items = items.OrderByDescending(x => x.date);
                    break;
                //case "contractor":
                //    if (direction1 == "up") items = items.OrderBy(x => x.contractorID);
                //    else items = items.OrderByDescending(x => x.contractorID);
                //    break;
                default:
                    break;
            }


            var total = items.Count();
            var res2 = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();



            return Json(new
            {
                items = res2.Select(x => new
                {
                    x.id,
                    date = x.date.ToString("dd.MM.yyyy"),
                    //x.contractorID,
                    //contractor = x.udovika_contractor != null ? x.udovika_contractor.name : "",
                    x.statusID,
                    status = x.udovika_statusEmail != null ? x.udovika_statusEmail.name : "",
                    trackNum = x.trackNum,
                    backNum = x.backNum,
                    from = x.from,
                    to = x.to,
                    system = x.system,
                    backDate = x.backDate.ToString("dd.MM.yyyy"),
                    description = x.description
                }),
                //total = total
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Mail_Create(string trackNum, string from, string to, 
            string backNum, string system, string description)
        {
            var msg = "";
            var user = mng.GetUser();
            var item = new udovika_email();
            item.id = 0;
            item.trackNum = trackNum;
            item.description = description;
            item.statusID = 1;
            item.date = DateTime.Now.Date;
            item.from = from;
            item.to = to;
            item.backDate = DateTime.Now.Date;
            item.backNum = backNum;
            item.system = system;

            var res = mng.Mail.SaveMail(item, user, out msg);
            
            return Json(new
            {
                result = item.id > 0,
                saveID = item.id,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MailInlineEdit(int pk, string value, string name)
        {
            string msg = "";
            var user = mng.GetUser();
            var res = mng.Mail.EditMailField(pk, name, value, out msg, user);
            if (!res)
            {
                return Json(new
                {
                    result = false,
                    msg = msg
                });
            }
            else
            {
                return Json(new
                {
                    result = true
                });
            }
        }
        public ActionResult GetMail(int id)
        {
            var msg = "";
            var user = mng.GetUser();
            var item = mng.Mail.GetMail(id, user, out msg);
            if (item != null)
            {
                return Json(new
                {
                    id = item.id,
                    date = item.date.ToString("dd.MM.yyyy"),
                    backDate = item.backDate.ToString("dd.MM.yyyy"),
                    trackNum = item.trackNum,
                    backNum = item.backNum,
                    from = item.from,
                    to = item.to,
                    system = item.system,
                    description = item.description,
                    statusID = item.statusID,
                });
            }
            else
            {
                return Json(new { msg = msg });
            }
        }
        public ActionResult EditMail(udovika_email mail)
        {
            var msg = "";
            var user = mng.GetUser();
            var res = mng.Mail.SaveMail(mail, user, out msg);
            if (res > 0)
                return Json(new { result = true });
            else
                return Json(new { result = false, msg = msg });
        }
        public ActionResult Mails_remove(int id)
        {
            string msg = "";
            var user = mng.GetUser();
            var result = mng.Mail.DeleteMail(id, user, out msg);
            if (!result) return Json(new { result = result, msg = msg });
            else return Json(new { result = result });
        }
        #endregion
    }
}
