using arkAS.BLL;
using RDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Glushko.BLL
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
        public List<gl_docs> GetDocuments()
        {
            var res = new List<gl_docs>();
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
        public gl_docs GetDocument(int id)
        {
            var res = new gl_docs();
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
        public void SaveDocument(gl_docs item)
        {
            try
            {
                _db.SaveDocument(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
        }

        public bool EditDocumentField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var document = new gl_docs();
            try
            {
                document = GetDocument(id);
                
                if (document != null)
                {
                    switch (code)
                    {
                        case "typeName":
                            document.docTypeID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "statusName":
                            document.docStatusID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "name":
                            document.name = value;
                            break;
                        case "number":
                            document.number = value;
                            break;
                        case "contragentName":
                            document.contragentID = RDL.Convert.StrToInt(value, 0);
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
            var res = false;
            try
            {
                RDL.CacheManager.PurgeCacheItems("gl_doc");
                res = _db.DeleteDocument(id);
                msg = "Успешно";
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }

        #endregion
        #region Doc Statuses
        public List<gl_docStatuses> GetDocumentStatuses()
        {
            var res = new List<gl_docStatuses>();
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
        public gl_docStatuses GetDocumentStatus(int id)
        {
            var res = new gl_docStatuses();
            var key = "gl_docStatuses" + id;
            if (CacheManager.EnableCaching && CacheManager.Cache[key] != null)
            {
                res = (gl_docStatuses)CacheManager.Cache[key];
            }
            else
            {
                try
                {
                    res = _db.GetDocumentStatuses().FirstOrDefault(x => x.id == id);
                    CacheManager.CacheData(key, res);
                }
                catch (Exception ex)
                {
                    _debug(ex, new { id }, "id");
                }
            }
            return res;
        }

        public int SaveDocumentStatus(gl_docStatuses item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveDocumentStatus(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }

        public bool DeleteDocumentStatus(int id, out string msg)
        {
            var res = false;
            try
            {
                RDL.CacheManager.PurgeCacheItems("gl_docStatus");
                res = _db.DeleteDocumentStatus(id);
                msg = "Успешно";
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }

        public bool EditDocumentStatusField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var documentStatus = new gl_docStatuses();
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
        #region Doc Type
        public List<gl_docTypes> GetDocumentTypes()
        {
            var res = new List<gl_docTypes>();
            try
            {
                res = _db.GetDocumentTypes().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public gl_docTypes GetDocumentType(int id)
        {
            var res = new gl_docTypes();
            try
            {
                res = _db.GetDocumentTypes().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public bool DeleteDocumentType(int id)
        {
            var res = false;
            try
            {
                res = _db.DeleteDocumentType(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        public int SaveDocumentType(gl_docTypes item, bool withSave = true)
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
        #endregion

        #region
        public List<fin_contragents> GetFinContragents()
        {
            var res = new List<fin_contragents>();
            res = _db.GetFinContragents().ToList();
            return res;
        }
        #endregion
    }
}