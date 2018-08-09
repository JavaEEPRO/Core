using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public class DocumentsManager: IDocumentsManager
    {
        #region System
        public IReposirory db;
        public IManager mng;
        private bool _disposed;

        public DocumentsManager(IReposirory db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            //this.Debug = debug;
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
                    if (db != null)
                        db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        private bool _CanChangeDocument(aspnet_Users user)
        {
            var res = false;

            if (user.UserName == "rosh@email.com" || user.UserName == "roshAdmin@email.com")
            {
                res = true;
            }
            return res;
        }

        //public bool CanCurrentUserChangeDocument()
        //{
        //    return _CanChangeDocument(mng.GetUser());
        //}
        #endregion

        #region documents
        public List<rosh_documents> GetDocuments()
        {
            var res = new List<rosh_documents>();
            try
            {
                res = db.GetDocuments().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_documents GetDocument(int id)
        {
            var res = new rosh_documents();
            try
            {
                res = db.GetDocument(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveDocument(rosh_documents res, aspnet_Users user, out string msg)
        {
            msg = "";
            try
            {
                if (!_CanChangeDocument(user))
                {
                    msg = "Нет прав для данной операции";
                }
                else
                {
                    db.SaveDocument(res);
                    msg = "Документ успешно сохранен!";

                    int savedDocID = res.id;
                    //ChangeDocument(savedDocID);
                }
            }

            catch (Exception ex)
            {
                //RDL.Debug.LogError(ex);
                _debug(ex, new { res }, "");
            }
        }

        public void DeleteDocument(int id, aspnet_Users user, out string msg)
        {
            msg = "";

            try
            {
                if (!_CanChangeDocument(user))
                {
                    msg = "Нет прав для данной операции";
                }
                else
                {
                    db.DeleteDocument(id);
                    msg = "Документ успешно удален";
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
                msg = "Сбой операции";
            }
        }

        public void EditDocField(int id, string name, string value)
        {
            //var user = mng.GetUser();
            //string msg = "";

            var item = db.GetDocument(id);
            switch (name)
            {
                case "docDate": if (value != "") item.docDate = Convert.ToDateTime(value); break;
                case "docTypeName": if (value != "") item.docTypeID = RDL.Convert.StrToInt(value, 0); break;
                case "docNumber": item.docNumber = value; break;
                case "contragentName": if (value != "") item.contragentID = RDL.Convert.StrToInt(value, 0); break;
                case "docStatusName": if (value != "") item.docStatusID = RDL.Convert.StrToInt(value, 0); break;
                case "amount": item.amount = RDL.Convert.StrToDecimal(value, 0); break;
                case "description": item.description = value; break;
            }
            //SaveDocument(item, user, out msg);
            db.SaveDocument(item);
        }

        //public void ChangeDocument(int savedDocID)
        //{
        //    if(savedDocID != 0)
        //    {
        //        var savedDoc = GetDocument(savedDocID);
        //        var DocStatus = GetDocStatus(savedDoc.docStatusID);

        //        var DocLog = new rosh_docLogs
        //        {
        //            date = DateTime.Now.Date,
        //            documentID = savedDoc.id,
        //            changes = savedDoc.docNumber + ", " + "статус: " + DocStatus.name + ", " + savedDoc.description,
        //        };
        //        SaveDocLog(DocLog);
        //    }
        //}
        
        #endregion

        #region docTypes
        public List<rosh_docTypes> GetDocTypes()
        {
            var res = new List<rosh_docTypes>();
            try
            {
                res = db.GetDocTypes().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_docTypes GetDocType(int id)
        {
            var res = new rosh_docTypes();
            try
            {
                res = db.GetDocType(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveDocType(rosh_docTypes res)
        {
            try
            {
                db.SaveDocType(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteDocType(int id)
        {
            try
            {
                db.DeleteDocType(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region docStatuses
        public List<rosh_docStatuses> GetDocStatuses()
        {
            var res = new List<rosh_docStatuses>();
            try
            {
                res = db.GetDocStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_docStatuses GetDocStatus(int id)
        {
            var res = new rosh_docStatuses();
            try
            {
                res = db.GetDocStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveDocStatus(rosh_docStatuses res)
        {
            try
            {
                db.SaveDocStatus(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteDocStatus(int id)
        {
            try
            {
                db.DeleteDocStatus(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region docLogs
        public List<rosh_docLogs> GetDocLogs()
        {
            var res = new List<rosh_docLogs>();
            try
            {
                res = db.GetDocLogs().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_docLogs GetDocLog(int id)
        {
            var res = new rosh_docLogs();
            try
            {
                res = db.GetDocLog(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveDocLog(rosh_docLogs res)
        {
            try
            {
                db.SaveDocLog(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteDocLog(int id)
        {
            try
            {
                db.DeleteDocLog(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion
    }
}