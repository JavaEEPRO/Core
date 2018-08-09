using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.BLL
{
    public class Repository : IRepository
    {
        #region System
        private LocalSqlServer _db;
        public LocalSqlServer db
        {
            get
            {
                if(_db == null)
                {
                    _db = new LocalSqlServer();
                }
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        private bool _disposed;
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
        protected virtual void Dispose (bool disposing)
        {
            if(!_disposed)
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
        #region Contragents
        public IQueryable<molchanov_contragents> GetContragents()
        {
            var res = db.molchanov_contragents;
            return res;
        }
        public molchanov_contragents GetContragent(int id)
        {
            var res = db.molchanov_contragents.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveContragent(molchanov_contragents element, bool withSave = true)
        {
            if(element.id == 0)
            {
                db.molchanov_contragents.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteContragent(int id)
        {
            bool res = false;
            var item = db.molchanov_contragents.SingleOrDefault(x => x.id == id);
            if(item!= null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Documents
        public IQueryable<molchanov_documents> GetDocuments()
        {
            var res = db.molchanov_documents
                        .Include(c => c.molchanov_contragents)
                        .Include(t => t.molchanov_docTypes)
                        .Include(s => s.molchanov_docStatuses);
            return res;
        }
        public molchanov_documents GetDocument(int id)
        {
            var res = db.molchanov_documents.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocument(molchanov_documents element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_documents.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocument(int id)
        {
            bool res = false;
            var item = db.molchanov_documents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region DocTypes
        public IQueryable<molchanov_docTypes> GetDocTypes()
        {
            var res = db.molchanov_docTypes;
            return res;
        }
        public molchanov_docTypes GetDocType(int id)
        {
            var res = db.molchanov_docTypes.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocType(molchanov_docTypes element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_docTypes.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocType(int id)
        {
            bool res = false;
            var item = db.molchanov_docTypes.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region DocStatuses
        public IQueryable<molchanov_docStatuses> GetDocStatuses()
        {
            var res = db.molchanov_docStatuses;
            return res;
        }
        public molchanov_docStatuses GetDocStatus(int id)
        {
            var res = db.molchanov_docStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocStatus(molchanov_docStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_docStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocStatus(int id)
        {
            bool res = false;
            var item = db.molchanov_docStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Invoices
        public IQueryable<molchanov_invoices> GetInvoices()
        {
            var res = db.molchanov_invoices.Include(s => s.molchanov_invoiceStatuses);
            return res;
        }
        public molchanov_invoices GetInvoice(int id)
        {
            var res = db.molchanov_invoices.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveInvoice(molchanov_invoices element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_invoices.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = db.molchanov_invoices.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region InvoiceStatuses
        public IQueryable<molchanov_invoiceStatuses> GetInvoiceStatuses()
        {
            var res = db.molchanov_invoiceStatuses;
            return res;
        }
        public molchanov_invoiceStatuses GetInvoiceStatus(int id)
        {
            var res = db.molchanov_invoiceStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveInvoiceStatus(molchanov_invoiceStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_invoiceStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteInvoiceStatus(int id)
        {
            bool res = false;
            var item = db.molchanov_invoiceStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Mails
        public IQueryable<molchanov_mails> GetMails()
        {
            var res = db.molchanov_mails
                .Include(s => s.molchanov_mailStatuses)
                .Include(d => d.molchanov_deliverySystems);           
            return res;
        }
        public molchanov_mails GetMail(int id)
        {
            var res = db.molchanov_mails.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveMail(molchanov_mails element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_mails.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteMail(int id)
        {
            bool res = false;
            var item = db.molchanov_mails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region MailStatuses
        public IQueryable<molchanov_mailStatuses> GetMailStatuses()
        {
            var res = db.molchanov_mailStatuses;
            return res;
        }
        public molchanov_mailStatuses GetMailStatus(int id)
        {
            var res = db.molchanov_mailStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveMailStatus(molchanov_mailStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_mailStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteMailStatus(int id)
        {
            bool res = false;
            var item = db.molchanov_mailStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region DeliverySystems
        public IQueryable<molchanov_deliverySystems> GetDeliverySystems()
        {
            var res = db.molchanov_deliverySystems;
            return res;
        }
        public molchanov_deliverySystems GetDeliverySystem(int id)
        {
            var res = db.molchanov_deliverySystems.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDeliverySystem(molchanov_deliverySystems element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_deliverySystems.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDeliverySystem(int id)
        {
            bool res = false;
            var item = db.molchanov_deliverySystems.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogContragents
        public IQueryable<molchanov_logContragents> GetContragentLogs()
        {
            var res = db.molchanov_logContragents.Include(c=> c.molchanov_contragents);
            return res;
        }
        public molchanov_logContragents GetContragentLog(int id)
        {
            var res = db.molchanov_logContragents.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveContragentLog(molchanov_logContragents element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logContragents.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteContragentLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logContragents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogDocuments
        public IQueryable<molchanov_logDocuments> GetDocumentLogs()
        {
            var res = db.molchanov_logDocuments
                        .Include(d => d.molchanov_documents);
            return res;
        }
        public molchanov_logDocuments GetDocumentLog(int id)
        {
            var res = db.molchanov_logDocuments.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocumentLog(molchanov_logDocuments element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logDocuments.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocumentLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logDocuments.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogDocTypes
        public IQueryable<molchanov_logDocTypes> GetDocTypesLogs()
        {
            var res = db.molchanov_logDocTypes.Include(t=> t.molchanov_docTypes);
            return res;
        }
        public molchanov_logDocTypes GetDocTypeLog(int id)
        {
            var res = db.molchanov_logDocTypes.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocTypeLog(molchanov_logDocTypes element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logDocTypes.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocTypeLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logDocTypes.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogDocStatuses
        public IQueryable<molchanov_logDocStatuses> GetDocStatusesLogs()
        {
            var res = db.molchanov_logDocStatuses.Include(s => s.molchanov_docStatuses);
            return res;
        }
        public molchanov_logDocStatuses GetDocStatusesLog(int id)
        {
            var res = db.molchanov_logDocStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocStausesLog(molchanov_logDocStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logDocStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDocStatusesLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logDocStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogInvoices
        public IQueryable<molchanov_logInvoices> GetInvoiceLogs()
        {

            var res = db.molchanov_logInvoices.Include(i => i.molchanov_invoices);
            return res;
        }
        public molchanov_logInvoices GetInvoiceLog(int id)
        {
            var res = db.molchanov_logInvoices.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveInvoiceLog(molchanov_logInvoices element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logInvoices.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteInvoiceLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logInvoices.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogInvoiceStatuses
        public IQueryable<molchanov_logInvoiceStatuses> GetInvoiceStautsLog()
        {
            var res = db.molchanov_logInvoiceStatuses.Include(i=> i.molchanov_invoiceStatuses);
            return res;
        }
        public molchanov_logInvoiceStatuses GetInvoiceStatusLog(int id)
        {
            var res = db.molchanov_logInvoiceStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveInvoiceStautsLog(molchanov_logInvoiceStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logInvoiceStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteInvoiceStatusLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logInvoiceStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogMails
        public IQueryable<molchanov_logMails> GetMailLogs()
        {
            var res = db.molchanov_logMails.Include(s => s.molchanov_mails);
            return res;
        }
        public molchanov_logMails GetMailLog(int id)
        {
            var res = db.molchanov_logMails.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveMailLog(molchanov_logMails element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logMails.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteMailLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logMails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogMailStatuses
        public IQueryable<molchanov_logMailStatuses> GetMailStatusLogs()
        {
            var res = db.molchanov_logMailStatuses.Include(l => l.molchanov_mailStatuses);
            return res;
        }
        public molchanov_logMailStatuses GetMailStatusLog(int id)
        {
            var res = db.molchanov_logMailStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveMailStatusLog(molchanov_logMailStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logMailStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteMailStatusLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logMailStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region LogDeliverySystems
        public IQueryable<molchanov_logDeliverySystems> GetDeliverySystemLogs()
        {
            var res = db.molchanov_logDeliverySystems.Include(s => s.molchanov_deliverySystems);
            return res;
        }
        public molchanov_logDeliverySystems GetDeliverySystemLog(int id)
        {
            var res = db.molchanov_logDeliverySystems.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDeliverySystemLog(molchanov_logDeliverySystems element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.molchanov_logDeliverySystems.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        public bool DeleteDeliverySystemLog(int id)
        {
            bool res = false;
            var item = db.molchanov_logDeliverySystems.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
    }
}