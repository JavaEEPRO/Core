using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
namespace arkAS.Areas.Podvashetskyi.BLL
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
        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region Documents
        public List<pdv_documents> GetDocuments()
        {
            var res = new List<pdv_documents>();
            try
            {
                res = _db.GetDocuments().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_documents GetDocument(int id)
        {
            var res = new pdv_documents();
            try
            {
                res = _db.GetDocuments().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = id }, "");
            }
            return res;
        }
        public void SaveDocument(pdv_documents item)
        {
            try
            {
                _db.SaveDocument(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = item.id }, "");
            }
        }
        public bool AddDocument(string number)
        {
            bool res = false;
            var document = new pdv_documents();
            try
            {
                document = new pdv_documents
                {
                    id = 0,
                    number = number,
                    createdDate = DateTime.Now,
                };
                SaveDocument(document);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public bool EditDocumentField(int id, string code, string value, aspnet_Users user, out string msg)
        {
            bool res = false;
            var document = new pdv_documents();
            try
            {
                document = GetDocument(id);
                //if(document.contractorID != user.id)
                //{
                //    msg = "Нет прав для редактирования документа";
                //    return res;
                //}
                if (document != null)
                {
                    switch(code)
                    {
                        case "number": document.number = value;
                            break;
                        case "statusName": document.documentStatusID = RDL.Convert.StrToInt(value, 0);
                            _logDocStatusChange(document, "Статус изменен " + user.UserName);
                            break;
                        case "name": document.name = value;
                            break;
                        case "typeName": document.documentTypeID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "contractorName":
                            document.contractorID = RDL.Convert.StrToInt(value, 0);
                            break;
                    }
                    SaveDocument(document);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти документ";
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        public bool DeleteDocument(int id, out string msg)
        {
            bool res = false;
            var document = new pdv_documents();
            try
            {
                document = GetDocument(id);
                res = _db.DeleteDocument(id);
                msg = "Докумнт удален";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Не удалось удалить документ";
            }
            return res;
        }
        private bool _logDocStatusChange(pdv_documents document, string note = "")
        {
            var res = false;
            try
            {
                _db.SaveDocStatusLog(new pdv_documentStatusesLog
                {
                    id = 0,
                    created = DateTime.Now,
                    statusID = document.documentStatusID,
                    documentID = document.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = document.id, statusID = document.documentStatusID, note });
            }
            return res;
        }
        #endregion
        #region Document Statuses
        public List<pdv_documentStatuses> GetDocumentStatuses()
        {
            var res = new List<pdv_documentStatuses>();
            try
            {
                res = _db.GetDocumentStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_documentStatuses GetDocumentStatus(int id)
        {
            var res = new pdv_documentStatuses();
            try
            {
                res = _db.GetDocumentStatuses().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int GetDocumentStatusCodeId(string code)
        {
            var res = new pdv_documentStatuses();
            try
            {
                res = _db.GetDocumentStatuses().FirstOrDefault(x => x.code == code);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res.id;
        }
        public int SaveDocumentStatus(pdv_documentStatuses item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveDocumentStatuses(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }
        public bool DeleteDocumentStatus(int id)
        {
            var res = false;
            try
            {
                //RDL.CacheManager.PurgeCacheItems("gl_docStatus");
                res = _db.DeleteDocumentStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        public bool EditDocumentStatusField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var documentStatus = new pdv_documentStatuses();
            try
            {
                documentStatus = GetDocumentStatus(id);

                if (documentStatus != null)
                {
                    switch (code)
                    {
                        case "name": documentStatus.name = value;
                            break;
                        case "code": documentStatus.code = value;
                            break;

                    }
                    SaveDocumentStatus(documentStatus);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти статус документ";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        #endregion 
        #region Document Type
        public List<pdv_documentType> GetDocumentTypes()
        {
            var res = new List<pdv_documentType>();
            try
            {
                res = _db.GetDocumentType().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_documentType GetDocumentType(int id)
        {
            var res = new pdv_documentType();
            try
            {
                res = _db.GetDocumentType().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveDocumentTypes(pdv_documentType item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveDocumentType(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }

        public bool DeleteDocumentTypes(int id)
        {
            var res = false;
            try
            {
                res = _db.DeleteDocumentTypes(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }

        public bool EditDocumentTypesField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var documentTypes = new pdv_documentType();
            try
            {
                documentTypes = GetDocumentType(id);

                if (documentTypes != null)
                {
                    switch (code)
                    {
                        case "name": documentTypes.name = value;
                            break;
                        case "code": documentTypes.code = value;
                            break;

                    }
                    SaveDocumentTypes(documentTypes);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти статус документ";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        #endregion 
  


}
}