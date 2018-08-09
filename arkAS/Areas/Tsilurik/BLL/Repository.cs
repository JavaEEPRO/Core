using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using System.Data.Entity;

namespace arkAS.Areas.Tsilurik.BLL
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

        #region Document
        public tsilurik_documents GetDocument(int id)
        {
            var res = Db.tsilurik_documents.FirstOrDefault(x => x.id == id);
            return res;
        }

        public IQueryable<tsilurik_documents> GetDocuments()
        {
            var res = Db.tsilurik_documents
                .Include(c => c.tsilurik_contractors)
                .Include(s => s.tsilurik_status)
                .Include(t => t.tsilurik_types);
            return res;
        }

        public int SaveDocument(tsilurik_documents item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.tsilurik_documents.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;
        }

        public int UpdateDocumentStatus(int id, int statusID, bool withSave = true)
        {
            var result = Db.tsilurik_documents.FirstOrDefault(x => x.id == id);
            if (result != null)
            {
                result.statusID = statusID;
                if (withSave) Save();
            }
            return result.id;
        }

        public IQueryable<tsilurik_types> GetDocumentTypes()
        {
            var res = Db.tsilurik_types;
            return res;
        }

        public int SaveLogsDocumentStatus(tsilurik_statusLog item)
        {
            if (item.id == 0)
            {
                Db.tsilurik_statusLog.Add(item);
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
        public tsilurik_invoices GetInvoice(int id)
        {
            var res = Db.tsilurik_invoices.FirstOrDefault(x => x.id == id);
            return res;
        }

        public IQueryable<tsilurik_invoices> GetInvoices()
        {
            var res = Db.tsilurik_invoices
                .Include(s => s.tsilurik_status)
                .Include(c => c.tsilurik_contractors);
            return res;
        }

        public int SaveInvoice(tsilurik_invoices item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.tsilurik_invoices.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;
        }

        public int UpdateInvoiceStatus(int id, int statusID, bool withSave = true)
        {
            var result = Db.tsilurik_invoices.FirstOrDefault(x => x.id == id);
            if (result != null)
            {
                result.statusID = statusID;
                if (withSave) Save();
            }
            return result.id;
        }

        public int SaveLogsInvoiceStatus(tsilurik_statusLog item)
        {
            if (item.id == 0)
            {
                Db.tsilurik_statusLog.Add(item);
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
        public tsilurik_mails GetMail(int id)
        {
            var res = Db.tsilurik_mails.FirstOrDefault(x => x.id == id);
            return res;
        }

        public IQueryable<tsilurik_mails> GetMails()
        {
            var res = Db.tsilurik_mails.Include(s => s.tsilurik_status);
            return res;
        }

        public int SaveMail(tsilurik_mails item, bool withSave = true)
        {
            if (item.id == 0)
            {
                Db.tsilurik_mails.Add(item);
                if (withSave) Save();
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
                if (withSave) Save();
            }
            return item.id;
        }

        public int UpdateMailStatus(int id, int statusID, bool withSave = true)
        {
            var result = Db.tsilurik_mails.FirstOrDefault(x => x.id == id);
            if (result != null)
            {
                result.statusID = statusID;
                if (withSave) Save();
            }
            return result.id;
        }

        public int SaveLogsMailStatus(tsilurik_statusLog item)
        {
            if (item.id == 0)
            {
                Db.tsilurik_statusLog.Add(item);
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
        public IQueryable<tsilurik_contractors> GetContractors()
        {
            var res = Db.tsilurik_contractors;
            return res;
        }
        #endregion

        #region Statuses
        public IQueryable<tsilurik_status> GetStatuses(string document)
        {
            var res = Db.tsilurik_status.Where(s => s.note == document);
            return res;
        }
        #endregion

    }
}