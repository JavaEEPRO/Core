using arkAS.Areas.Molchanov.Infrastructure;
using arkAS.BLL;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.BLL
{
    public class DocumentsManager : IDocumentsManager
    {
        #region System
        private IRepository _db;
        private IManager _mng;
        private bool _disposed;
        public DocumentsManager(IRepository db, IManager mng)
        {
            _db = db;
            _mng = mng;
            _disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                        _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        private bool _canAccessToItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            var isManager = user.aspnet_Roles.Any(x => x.RoleName == "guest");
            if (user != null && isAdmin || isManager)
            {
                res = true;
            }
            return res;
        }
        private bool _canManageItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            if (user != null && isAdmin)
            {
                res = true;
            }
            return res;
        }
        private molchanov_logDocuments _logDocumentChanges(aspnet_Users user, molchanov_documents element, string notice)
        {
            var res = new molchanov_logDocuments { date = DateTime.Now, userName = user.UserName, notice = "{element.number}: " + notice, documentID = element.id };
            return res;
        }
        private molchanov_logDocTypes _logDocumentTypeChanges(aspnet_Users user, molchanov_docTypes element, string notice)
        {
            var res = new molchanov_logDocTypes {date = DateTime.Now, userName = user.UserName, notice = "{element.name}: " + notice, docTypeID = element.id };
            return res;
        }
        private molchanov_logDocStatuses _logDocStatusesChanges(aspnet_Users user, molchanov_docStatuses element, string notice)
        {
            var res = new molchanov_logDocStatuses {date = DateTime.Now, userName = user.UserName, notice ="{element.name}: " + notice, docStatusID = element.id };
            return res;
        }
        #endregion
        #region Documents
        public List<molchanov_documents> GetDocuments(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_documents> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                   
                    res = _db.GetDocuments().Where(x => x.isDeleted != true).ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }
        public molchanov_documents GetDocument(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_documents res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetDocument(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_documents CreateDocument(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_documents res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_documents();
                    res.uniqueCode = Guid.NewGuid();
                    res.isDeleted = false;
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "number")
                        {
                            res.number = parameters[key];
                        }
                        else if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "sum")
                        {
                            res.sum = RDL.Convert.StrToDecimal(parameters[key], 0);
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "link")
                        {
                            res.link = parameters[key];
                        }
                        else if (key == "contragentName")
                        {
                            res.contragentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "docStatus")
                        {
                            res.docStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "docTypes")
                        {
                            res.docTypeID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "ParentDocs")
                        {
                            res.docParentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                        _db.SaveDocument(res);
                    _db.SaveDocumentLog(_logDocumentChanges(user, res, "Документ создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_documents EditDocument(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_documents res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetDocument(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "number")
                        {
                            res.number = parameters[key];
                        }
                        else if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "sum")
                        {
                            res.sum = RDL.Convert.StrToDecimal(parameters[key], 0);
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "link")
                        {
                            res.link = parameters[key];
                        }
                        else if (key == "contragentName")
                        {
                            res.contragentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "docStatus")
                        {
                            res.docStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "docTypes")
                        {
                            res.docTypeID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "ParentDocs")
                        {
                            res.docParentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                    _db.SaveDocument(res);
                    _db.SaveDocumentLog(_logDocumentChanges(user, res, "Документ  отредактирован"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }
        public bool ChangeDocumentInline(int id, string name, string value, out string msg, aspnet_Users user)
        {
            bool res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = false;
                }
                else
                {
                    var item = _db.GetDocument(id);
                    switch (name)
                    {
                        case "date":
                            item.date = Convert.ToDateTime(value);
                            _db.SaveDocument(item);
                            _db.SaveDocumentLog(_logDocumentChanges(user, item, "Дата изменена. Новая дата: {item.date.ToShortDateString()}"));
                            res = true;
                            break;
                        case "sum":
                            item.sum = RDL.Convert.StrToDecimal(value, 0);
                            _db.SaveDocument(item);
                            _db.SaveDocumentLog(_logDocumentChanges(user, item, "Сумма изменена. Новая сумма:  {item.sum}"));
                            res = true;
                            break;
                        case "link":
                            item.link = value;
                            _db.SaveDocument(item);
                            _db.SaveDocumentLog(_logDocumentChanges(user, item, "Ссылка  изменена. Новая ссылка:  {item.link}"));
                            res = true;
                            break;
                        case "invoiceStatus":
                            int stID = RDL.Convert.StrToInt(value, 0);
                            item.docStatusID = stID;
                            _db.SaveDocument(item);
                            _db.SaveDocumentLog(_logDocumentChanges(user, item, "Статус изменен. Новый статус: {item.molchanov_docStatuses.name}"));
                            res = true;
                            break;
                        default:
                            res = false;
                            break;
                    }                    
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = false;
            }
            return res;
        }

        public bool RemoveDocument(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.GetDocument(id).isDeleted = true;
                    _db.SaveDocumentLog(_logDocumentChanges(user, _db.GetDocument(id), "Документ удален"));
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }

        public List<molchanov_logDocuments> GetDocLogsByID (int id)
        {
            List <molchanov_logDocuments> res;
            try
            {
                res = _db.GetDocumentLogs().Where(x => x.documentID == id).ToList();
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка");
                res = null;
            }
            return res;
        }
        #endregion
        #region DocTypes
        public List<molchanov_docTypes> GetDocTypes(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_docTypes> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetDocTypes().ToList();
                }


            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_docTypes GetDocType(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_docTypes res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetDocType(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_docTypes CreateDocType(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_docTypes res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_docTypes();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                        _db.SaveDocType(res);
                    _db.SaveDocTypeLog(_logDocumentTypeChanges(user, res, "Тип документа создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }
        public molchanov_docTypes EditDocType(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_docTypes res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetDocType(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                        _db.SaveDocType(res);
                    _db.SaveDocTypeLog(_logDocumentTypeChanges(user, res, "Тип документа  изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }

        public bool RemoveDocType(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.DeleteDocType(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        #endregion
        #region DocStatuses
        public List<molchanov_docStatuses> GetDocStatuses(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_docStatuses> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetDocStatuses().ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_docStatuses GetDocStatus(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_docStatuses res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetDocStatus(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_docStatuses CreateDocStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_docStatuses res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_docStatuses();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                        _db.SaveDocStatus(res);
                    _db.SaveDocStausesLog(_logDocStatusesChanges(user, res, "Статус документа создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_docStatuses EditDocStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_docStatuses res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetDocStatus(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                        _db.SaveDocStatus(res);
                    _db.SaveDocStausesLog(_logDocStatusesChanges(user, res, "Статус документа изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }

        public bool RemoveDocStatus(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.DeleteDocStatus(id);
                    msg = "Статус документа удален";
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        #endregion
    }            
}