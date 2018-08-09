using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public class Repository : IRepository
    {
        #region System
        private LocalSqlServer _db;
        private bool _disposed;
        public LocalSqlServer Db
        {
            get
            {

                if (_db == null)
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
        public Repository(LocalSqlServer db)
        {
            if (db == null) this.Db = new LocalSqlServer();
            _disposed = false;
        }
        public void Save()
        {
            Db.SaveChanges();
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
                    if (Db != null) Db.Dispose();
                }
                Db = null;
                _disposed = true;
            }
        }
        #endregion

        #region Documents
        public vas_documents GetDocument(int id)
        {
            var res = Db.vas_documents.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_documents> GetDocuments()
        {
            var res = Db.vas_documents
                .Include(c => c.vas_contractors)
                .Include(s => s.vas_docStatuses)
                .Include(t => t.vas_docTypes);
            return res;
        }
        public int SaveDocument(vas_documents item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_documents.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteDocument(int id)
        {
            bool res = false;
            var item = Db.vas_documents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                item.isDeleted = true;
                Db.Entry(item).State = EntityState.Modified;
                Save();
                res = true;
            }
            return res;
        }
        public int SaveDocStatusLog(vas_docStatusesLog item)
        {
            if (item.id == 0)
            {
                Db.vas_docStatusesLog.Add(item);
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return item.id;
        }

        #endregion

        #region Invoices
        public vas_invoices GetInvoice(int id)
        {
            var res = Db.vas_invoices.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_invoices> GetInvoices()
        {
            var res = Db.vas_invoices
                .Include(s => s.vas_invoiceStatuses)
                .Include(c => c.vas_contractors);
            return res;
        }
        public int SaveInvoice(vas_invoices item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_invoices.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = Db.vas_invoices.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        public int SaveInvoiceStatusLog(vas_invoiceStatusesLog item)
        {
            if (item.id == 0)
            {
                Db.vas_invoiceStatusesLog.Add(item);
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return item.id;
        }
        #endregion

        #region Mails
        public vas_mails GetMail(int id)
        {
            var res = Db.vas_mails.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_mails> GetMails()
        {
            var res = Db.vas_mails.Include(s => s.vas_mailStatuses);
            return res;
        }
        public int SaveMail(vas_mails item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_mails.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteMail(int id)
        {
            bool res = false;
            var item = Db.vas_mails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        public int SaveMailStatusLog(vas_mailStatusesLog item)
        {
            if (item.id == 0)
            {
                Db.vas_mailStatusesLog.Add(item);
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return item.id;
        }
    
        #endregion

        #region Contractors
        public vas_contractors GetContractor(int id)
        {
            var res = Db.vas_contractors.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_contractors> GetContractors()
        {
            var res = Db.vas_contractors;
            return res;
        }
        public int SaveContractor(vas_contractors item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_contractors.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteContractor(int id)
        {
            bool res = false;
            var item = Db.vas_contractors.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                item.isDeleted = true;
                Db.Entry(item).State = EntityState.Modified;
                Save();
                res = true;
            }
            return res;
        }
        #endregion

        #region Document Statuses
        public vas_docStatuses GetDocStatus(int id)
        {
            var res = Db.vas_docStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_docStatuses> GetDocStatuses()
        {
            var res = Db.vas_docStatuses;
            return res;
        }
        public int SaveDocStatus(vas_docStatuses item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_docStatuses.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteDocStatus(int id)
        {
            bool res = false;
            var item = Db.vas_docStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion

        #region Document Types
        public vas_docTypes GetDocType(int id)
        {
            var res = Db.vas_docTypes.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_docTypes> GetDocTypes()
        {
            var res = Db.vas_docTypes;
            return res;
        }
        public int SaveDocType(vas_docTypes item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_docTypes.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteDocType(int id)
        {
            bool res = false;
            var item = Db.vas_docTypes.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion

        #region Invoice Statuses
        public vas_invoiceStatuses GetInvoiceStatus(int id)
        {
            var res = Db.vas_invoiceStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_invoiceStatuses> GetInvoiceStatuses()
        {
            var res = Db.vas_invoiceStatuses;
            return res;
        }
        public int SaveInvoiceStatus(vas_invoiceStatuses item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_invoiceStatuses.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteInvoiceStatus(int id)
        {
            bool res = false;
            var item = Db.vas_invoiceStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion

        #region Mail Statuses
        public vas_mailStatuses GetMailStatus(int id)
        {
            var res = Db.vas_mailStatuses.FirstOrDefault(x => x.id == id);
            return res;
        }
        public IQueryable<vas_mailStatuses> GetMailStatuses()
        {
            var res = Db.vas_mailStatuses;
            return res;
        }
        public int SaveMailStatus(vas_mailStatuses item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.vas_mailStatuses.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;

        }
        public bool DeleteMailStatus(int id)
        {
            bool res = false;
            var item = Db.vas_mailStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
    }
}
