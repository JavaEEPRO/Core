using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public class DocumentsManager : IDocumentsManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool disposed;

        public DocumentsManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region Document
        public List<udovika_contract> GetDocuments()
        {
            var res = new List<udovika_contract>();
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
        public bool GetPermissionAccessDocument(aspnet_Users user)
        {
            var res = false;
            if ( user != null && user.UserName == "core-guest@mail.ru")
            {
                return true;
            }
            return res;
        }
        public bool WriteChangeDocumentStatus(udovika_contract doc, string note = "")
        {
            var res = false;
            try
            {
                db.SaveDocumentStatusLog(new udovika_documentStatusLog { 
                    id = 0,
                    documentID = doc.id,
                    note = note,
                    statusID = doc.statusID,
                    created = DateTime.Now
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = doc.id, statusID = doc.statusID, note }, "");
            }
            return res;
        }
        public udovika_contract GetDocument(int id, aspnet_Users user, out string msg)
        {
            var res = new udovika_contract();
            msg = "";
            try
            {
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return res = null;
                }
                res = db.GetDocuments().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = id}, "");
            }
            return res;
        }
        public bool CreateDocument(string name, aspnet_Users user, out string msg)
        {
            var res = false;
            var doc = new udovika_contract();
            msg = "";
            try
            {
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return false;
                }
                else
                {
                    doc = new udovika_contract() { id = 0, number = name, date = DateTime.Now };
                    SaveDocument(doc, user, out msg);
                    res = true;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, doc, "");
            }
            return res;
        }
        public int SaveDocument(udovika_contract item, aspnet_Users user, out string msg)
        {
            msg = "";
            try
            {
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return 0;
                }else
                    db.SaveDocument(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");   
            }
            return item.id;
        }
        public bool EditDocument(int id, string value, aspnet_Users user, out string msg)
        {
            bool res = false;
            var doc = new udovika_contract();
            msg = "";
            try
            {
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return res;
                }
                else
                {
                    doc = db.GetDocument(id);
                    if (doc != null)
                    {
                        doc.number = value;
                        SaveDocument(doc, user, out msg);
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _debug(ex, doc, "");   
            }
            return res;
        }
        public bool DeleteDocument(int id, aspnet_Users user, out string msg)
        {
            bool res = false;
            //var item = new udovika_contract();
            try
            {
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав.";
                    return res;
                }
                else
                {
                    db.DeleteDocument(id);
                    msg = "Документ удален.";
                    res = true;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { docId = id }, "");
                msg = "Документ не удален.";
            }
            return res;
        }
        public bool EditDocumentField(int id, string code, 
            string value, out string msg, aspnet_Users user)
        {
            bool res = false;
            var doc = new udovika_contract();
            msg = "";
            try
            {
                doc = db.GetDocument(id);
                if (!GetPermissionAccessDocument(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return res;
                }
                if (doc != null)
                {
                    switch (code)
                    {
                        case "type":
                            doc.typeID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "status":
                            doc.statusID = RDL.Convert.StrToInt(value, 0);
                            WriteChangeDocumentStatus(doc, "Статус изменен " + user.UserName);
                            break;
                        case "link":
                            doc.link = value;
                            break;
                        case "number":
                            doc.number = value;
                            break;
                        case "contractor":
                            doc.contractorID = RDL.Convert.StrToInt(value, 0);
                            break;

                    }
                    SaveDocument(doc, user, out msg);
                    res = true;
                    msg = "Сохранено.";
                }
                else
                {
                    msg = "Ошибка. Нету такого документа.";
                }
            }
            catch (Exception ex)
            {
                _debug(ex,doc,"");
                msg = "Ошибка.";
            }
            return res;
        }
        #endregion
        #region DocumentStatus
        public udovika_statusContract GetStatusDocument(int id)
        {
            var res = new udovika_statusContract();
            try
            {
                res = db.GetDocumentStatuses().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveSatusDocument(udovika_statusContract status)
        {
            var res = 0;
            try
            {
                res = db.SaveSatusDocument(status);
            }
            catch (Exception ex)
            {
                _debug(ex, new { status }, "statusField");
            }
            return res;
        }
        public List<udovika_statusContract> GetDocumentStatuses()
        {
            var res = new List<udovika_statusContract>();
            try
            {
                res = db.GetDocumentStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public bool EditDocumentStatusField(int id, string name, 
            string value, out string msg)
        {
            var res = false;
            var doc = new udovika_statusContract();
            try
            {
                doc = GetStatusDocument(id);
                if (doc != null)
                {
                    switch (name)
                    {
                        case "name":
                            doc.name = value;
                            break;
                    }
                    res = true;
                    msg = "Статус сохранен успешно.";
                }
                else
                    msg = "Ошибка.";

            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Ошибка. Статус не изменен.";
            }
            return res;
        }
        #endregion
        #region DocumentType
        public udovika_contractType GetDocumentType(int id)
        {
            var res = new udovika_contractType();
            try
            {
                res = db.GetDocumentTypes().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public List<udovika_contractType> GetDocumentTypes()
        {
            var res = new List<udovika_contractType>();
            try
            {
                res = db.GetDocumentTypes().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public bool EditDocumentTypeField(int id, string code, 
            string value, out string msg)
        {
            bool res = false;
            var docType = new udovika_contractType();
            try
            {
                docType = GetDocumentType(id);

                if (docType != null)
                {
                    switch (code)
                    {
                        case "name": docType.name = value;
                            break;
                    }
                    SaveDocumentType(docType);
                    res = true;
                    msg = "Успешно.";
                }
                else
                    msg = "Ошибка. Неизвестный статус.";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Ошибка. Тип документа не сохранен.";
            }
            return res;
        }
        public int SaveDocumentType(udovika_contractType item)
        {
            var res = 0;
            try
            {
                res = db.SaveDocumentType(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }
        #endregion
    }
}