//#define PSEUDO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using System.Data.Entity;
using arkAS.Areas.Motskin.Infrastructura;

namespace arkAS.Areas.Motskin.BLL
{
    public class Repository: IRepository
    {

        #region System
        private LocalSqlServer _db;
        private bool _disposed;
        public LocalSqlServer db
        {
            get
            {
                if (_db == null)
                    _db = new LocalSqlServer();
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        public Repository(LocalSqlServer db)
        {
            if (db == null) this.db = new LocalSqlServer();
            _disposed = false;
        }
        public void Save()
        {
            db.SaveChanges();
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
                    if (db != null) db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }
        #endregion

        #region Documents
        public IQueryable<motskin_documents> GetDocuments()
        {
#if !PSEUDO
            var res = db.motskin_documents
                .Include(c => c. motskin_contractors)
                .Include(s => s.motskin_documentStatuses)
                .Include(t => t.motskin_documentTypes);
            return res;
#else
            return PseudoData.GetDocuments();
#endif
        }

        public motskin_documents GetDocument(int id)
        {
#if !PSEUDO
            var res = db.motskin_documents.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetDocuments().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveDocument(motskin_documents element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_documents.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveDocument(element);
#endif
        }

        public bool DeleteDocument(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_documents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                item.isDeleted = true;
                db.Entry(item).State = EntityState.Modified;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteDocument(id);
#endif
        }
#endregion

        #region Invoices
        public IQueryable<motskin_invoices> GetInvoices()
        {
#if !PSEUDO
            var res = db.motskin_invoices
                .Include(s => s.motskin_invoiceStatuses)
                .Include(c => c.motskin_contractors);
            return res;
#else
            return PseudoData.GetInvoices();
#endif
        }

        public motskin_invoices GetInvoice(int id)
        {
#if !PSEUDO
            var res = db.motskin_invoices.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetInvoices().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveInvoice(motskin_invoices element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_invoices.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveInvoice(element);
#endif

        }

        public bool DeleteInvoice(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_invoices.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                item.isDeleted = true;
                db.Entry(item).State = EntityState.Modified;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteInvoice(id);
#endif
        }
#endregion

        #region Mail
        public IQueryable<motskin_mails> GetMails()
        {
#if !PSEUDO
            var res = db.motskin_mails
                .Include(s => s.motskin_mailStatuses)
                .Include(t => t.motskin_sendSystems);
            return res;
#else
            return PseudoData.GetMails();
#endif
        }

        public motskin_mails GetMail(int id)
        {
#if !PSEUDO
            var res = db.motskin_mails.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetMails().FirstOrDefault(x => x.id == id);;
#endif
        }

        public int SaveMail(motskin_mails element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_mails.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveMail(element);
#endif
        }

        public bool DeleteMail(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_mails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                item.isDeleted = true;
                db.Entry(item).State = EntityState.Modified;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteMail(id);
#endif
        }
        #endregion

        #region DocumentStatuses
        public IQueryable<motskin_documentStatuses> GetDocumentStatuses()
        {
#if !PSEUDO
            var res = db.motskin_documentStatuses;
            return res;
#else
            return PseudoData.GetDocumentStatuses();
#endif
        }

        public motskin_documentStatuses GetDocumentStatuse(int id)
        {
#if !PSEUDO
            var res = db.motskin_documentStatuses.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetDocumentStatuses().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveDocumentStatus(motskin_documentStatuses element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_documentStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveDocumentStatuses(element);
#endif
        }

        public bool DeleteDocumentStatus(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_documentStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteDocumentStatuses(id);
#endif
        }
        #endregion

        #region InvoiceStatuses
        public IQueryable<motskin_invoiceStatuses> GetInvoiceStatuses()
        {
#if !PSEUDO
            var res = db.motskin_invoiceStatuses;
            return res;
#else
            return PseudoData.GetInvoiceStatuses();
#endif
        }

        public motskin_invoiceStatuses GetInvoiceStatuse(int id)
        {
#if !PSEUDO
            var res = db.motskin_invoiceStatuses.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetInvoiceStatuses().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveInvoiceStatus(motskin_invoiceStatuses element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_invoiceStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveInvoiceStatuses(element);
#endif
        }

        public bool DeleteInvoiceStatus(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_invoiceStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteInvoiceStatuses(id);
#endif
        }
        #endregion

        #region MailStatuses
        public IQueryable<motskin_mailStatuses> GetMailStatuses()
        {
#if !PSEUDO
            var res = db.motskin_mailStatuses;
            return res;
#else
            return PseudoData.GetMailStatuses();
#endif
        }

        public motskin_mailStatuses GetMailStatuse(int id)
        {
#if !PSEUDO
            var res = db.motskin_mailStatuses.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetMailStatuses().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveMailStatus(motskin_mailStatuses element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_mailStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveMailStatuses(element);
#endif
        }
        public bool DeleteMailStatus(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_mailStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteMailStatuses(id);
#endif
        }
        #endregion

        #region Contractors
        public IQueryable<motskin_contractors> GetContractors()
        {
#if !PSEUDO
            var res = db.motskin_contractors;
            return res;
#else
            return PseudoData.GetContractors();
#endif
        }

        public motskin_contractors GetContractor(int id)
        {
#if !PSEUDO
            var res = db.motskin_contractors.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetContractors().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveContractor(motskin_contractors element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_contractors.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveContractor(element);
#endif
        }

        public bool DeleteContractor(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_contractors.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteContractor(id);
#endif
        }
#endregion

        #region DocumentTypes
        public IQueryable<motskin_documentTypes> GetDocumentTypes()
        {
#if !PSEUDO
            var res = db.motskin_documentTypes;
            return res;
#else
            return PseudoData.GetDocumentTypes();
#endif
        }
        public motskin_documentTypes GetDocumentType(int id)
        {
#if !PSEUDO
            var res = db.motskin_documentTypes.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetDocumentTypes().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveDocumentType(motskin_documentTypes element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_documentTypes.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveDocumentTypes(element);
#endif
        }

        public bool DeleteDocumentType(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_documentTypes.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteDocumentTypes(id);
#endif
        }

        #endregion

        #region SendSystems
        public IQueryable<motskin_sendSystems> GetSendSystems()
        {
#if !PSEUDO
            var res = db.motskin_sendSystems;
            return res;
#else
            return PseudoData.GetSendSystems();
#endif
        }

        public motskin_sendSystems GetSendSystem(int id)
        {
#if !PSEUDO
            var res = db.motskin_sendSystems.FirstOrDefault(x => x.id == id);
            return res;
#else
            return PseudoData.GetSendSystems().FirstOrDefault(x => x.id == id);
#endif
        }

        public int SaveSendSystem(motskin_sendSystems element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_sendSystems.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveSendSystem(element);
#endif

        }

        public bool DeleteSendSystem(int id)
        {
#if !PSEUDO
            bool res = false;
            var item = db.motskin_sendSystems.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
#else
            return PseudoData.DeleteSendSystem(id);
#endif
        }
        #endregion

        #region DocumentStatusLog
        public IQueryable<motskin_documentStatusLog> GetDocumentStatusLog()
        {
#if !PSEUDO
            var res = db.motskin_documentStatusLog
                .Include(c => c.motskin_documents)
                .Include(s => s.motskin_documentStatuses);
            return res;
#else
            return PseudoData.GetDocumentStatusLog();
#endif
        }

        public int SaveDocumentStatusLog(motskin_documentStatusLog element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_documentStatusLog.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveDocumentStatusLog(element);
#endif
        }
#endregion

        #region InvoiceStatusLog
        public IQueryable<motskin_invoiceStatusLog> GetInvoiceStatusLog()
        {
#if !PSEUDO
            var res = db.motskin_invoiceStatusLog
                .Include(c => c.motskin_invoices)
                .Include(s => s.motskin_invoiceStatuses);
            return res;
#else
            return PseudoData.GetInvoiceStatusLog();
#endif
        }

        public int SaveInvoiceStatusLog(motskin_invoiceStatusLog element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_invoiceStatusLog.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveInvoiceStatusLog(element);
#endif
        }
        #endregion

        #region MailStatusLog
        public IQueryable<motskin_mailStatusLog> GetMailStatusLog()
        {
#if !PSEUDO
            var res = db.motskin_mailStatusLog
                .Include(c => c.motskin_mails)
                .Include(s => s.motskin_mailStatuses);
            return res;
#else
            return PseudoData.GetMailStatusLog();
#endif
        }

        public int SaveMailStatusLog(motskin_mailStatusLog element, bool withSave = true)
        {
#if !PSEUDO
            if (element.id == 0)
            {
                db.motskin_mailStatusLog.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
#else
            return PseudoData.SaveMailStatusLog(element);
#endif
        }
        #endregion
    }
}