using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public class DocumentManager : IDocumentManager
    {
        #region System

        public IRepository db;
        public IManager mng;
        private bool _disposed;

        public DocumentManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

            }
            _disposed = true;
        }
        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, "", parameters);
        }
        #endregion

        #region Documents
        private IQueryable<vas_documents> _getDocuments()
        {
            return db.GetDocuments().Where(x => !x.isDeleted.HasValue || x.isDeleted == false).OrderBy(x => x.date);
        }
        private bool _canAccessToDocument(aspnet_Users user, vas_documents item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_documents))
            {
                return true;
            }
            return res;
        }
        private bool _canManageDocument(aspnet_Users user, vas_documents item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_documents))
            {
                return true;
            }
            return res;
        }
        public bool _logDocStatusChange(vas_documents item, string note = "")
        {
            var res = false;
            try
            {
                db.SaveDocStatusLog(new vas_docStatusesLog
                    {
                        id = 0,
                        created = DateTime.Now,
                        statusID = item.statusID,
                        documentID = item.id,
                        note = note
                    });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = item.id, statusID = item.statusID, note });
            }
            return res;
        }
        private string _getRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public vas_documents GetDocument(int id, aspnet_Users user, out string msg)
        {
            var res = new vas_documents();
            msg = "";
            try
            {
                res = db.GetDocument(id);
                if (!_canAccessToDocument(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public List<vas_documents> GetDocuments(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg)
        {
            msg = "";
            var res = new List<vas_documents>();

            if (!_canAccessToDocument(user, null))
            {
                msg = "Нет прав для данной операции";
                return res = null;
            }

            try
            {
                var items = _getDocuments();
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    var name = parameters.filter.ContainsKey("name") ? parameters.filter["name"].ToString() : "";
                    var typeID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["typeID"].ToString(), 0) : 0;
                    var contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                    List<int?> statusIDs = new List<int?>();
                    if (parameters.filter.ContainsKey("statusIDs"))
                    {
                        statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    }
                    DateTime createdMin = DateTime.MinValue, createdMax = DateTime.MaxValue;
                    if (parameters.filter.ContainsKey("date") && parameters.filter["date"] != null)
                    {
                        var dates = parameters.filter["date"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dates.Length > 0)
                        {
                            createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), DateTime.MinValue);
                        }
                        if (dates.Length > 1)
                        {
                            createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), DateTime.MaxValue);
                        }
                    }

                    items = items.Where(x => (typeID == 0 || x.typeID == typeID) &&
                        (contractorID == 0 || x.contractorID == contractorID) &&
                        (statusIDs.Count == 0 || statusIDs.Contains(x.statusID)) &&
                        (createdMin <= x.date && x.date <= createdMax)
                        );

                    if (name != "")
                    {
                        items = items.ToList().Where(x => x.name != null && x.name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                switch (sort1)
                {
                    case "name":
                        if (direction1 == "up") items = items.OrderBy(x => x.name);
                        else items = items.OrderByDescending(x => x.name);
                        break;
                    case "number":
                        if (direction1 == "up") items = items.OrderBy(x => x.number);
                        else items = items.OrderByDescending(x => x.number);
                        break;
                    case "date":
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                    case "sum":
                        if (direction1 == "up") items = items.OrderBy(x => x.sum);
                        else items = items.OrderByDescending(x => x.sum);
                        break;
                    case "type":
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_docTypes.name);
                        else items = items.OrderByDescending(x => x.vas_docTypes.name);
                        break;
                    case "status":
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_docStatuses.name);
                        else items = items.OrderByDescending(x => x.vas_docStatuses.name);
                        break;
                    case "contractor":
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_contractors.name);
                        else items = items.OrderByDescending(x => x.vas_contractors.name);
                        break;
                    default:
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                }
                res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public vas_documents CreateDocument(string name, int typeID, int contractorID, aspnet_Users user, out string msg)
        {
            var res = new vas_documents();
            msg = "";
            try
            {
                if (!_canManageDocument(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }

                res.name = name;
                res.number = _getRandomString(8); ;
                res.date = DateTime.Now;
                res.sum = 0.0m;
                res.typeID = typeID;
                res.statusID = 1;
                res.contractorID = contractorID;
                res.isDeleted = false;
                res.link = "Ссылка на документ";
                res.comment = "Комментарий";
                res.code = _getRandomString(8).ToLower();
                res.isDeleted = false;

                db.SaveDocument(res);

            }
            catch (Exception ex)
            {
                _debug(ex, new { name = name, typeID = typeID, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public bool DeleteDocument(int id, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetDocument(id);
                if (!_canManageDocument(user, item))
                {
                    msg = "У вас нет прав на данную операцию";
                    return res;
                }
                if (!db.DeleteDocument(id))
                {
                    throw new Exception("Сбой при удалении записи с базы");
                }

                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }
        public bool EditDocumentField(int pk, string name, string value, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetDocument(pk);
                if (!_canManageDocument(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                switch (name)
                {
                    case "name": item.name = value; break;
                    case "number": item.number = value; break;
                    case "sum": if (value != "") item.sum = RDL.Convert.StrToDecimal(value, 0); break;
                    case "comment": item.comment = value; break;
                    case "link": item.link = value; break;
                    case "type": if (value != "") item.typeID = RDL.Convert.StrToInt(value, 0); break;
                    case "contractor": if (value != "") item.contractorID = RDL.Convert.StrToInt(value, 0); break;
                    case "status": if (value != "") item.statusID = RDL.Convert.StrToInt(value, 0);
                        _logDocStatusChange(item, "Статус изменен " + user.UserName);
                        break;
                }
                db.SaveDocument(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { doocumentID = pk, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }
        
        #endregion

        #region Document Statuses

        public vas_docStatuses GetDocStatus(int id)
        {
            var res = new vas_docStatuses();
            try
            {
                res = db.GetDocStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docStatusID = id });
                res = null;
            }

            return res;
        }

        public List<vas_docStatuses> GetDocStatuses()
        {
            var res = new List<vas_docStatuses>();
            try
            {
                res = db.GetDocStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }
            return res;
        }

        public vas_docStatuses CreateDocStatus(string name, string code)
        {
            var res = new vas_docStatuses();

            try
            {
                res.name = name;
                res.code = code;
                db.SaveDocStatus(res);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docStatusName = name, docStatusCode = code });
                res = null;
            }

            return res;
        }

        public void DeleteDocStatus(int id)
        {
            try
            {
                db.DeleteDocStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docStatusID = id });
            }
        }

        #endregion

        #region Document Types
        public vas_docTypes GetDocType(int id)
        {
            var res = new vas_docTypes();
            try
            {
                res = db.GetDocType(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docTypeID = id });
                res = null;
            }
            return res;
        }

        public List<vas_docTypes> GetDocTypes()
        {
            var res = new List<vas_docTypes>();
            try
            {
                res = db.GetDocTypes().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }

        public vas_docTypes CreateDocType(string name, string code)
        {
            var res = new vas_docTypes();

            try
            {
                res.name = name;
                res.code = code;
                db.SaveDocType(res);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docTypeName = name, docTypeCode = code });
                res = null;
            }

            return res;
        }

        public void DeleteDocType(int id)
        {
            try
            {
                db.DeleteDocType(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { docTypeID = id });
            }
        }

        #endregion
    }
}