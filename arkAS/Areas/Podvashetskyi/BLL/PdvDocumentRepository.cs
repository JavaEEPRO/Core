using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using System.Data.Entity;
using System.Data.Entity.Core;
namespace arkAS.Areas.Podvashetskyi.BLL
{
    public class PdvDocumentRepository : IRepository
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
        public PdvDocumentRepository(LocalSqlServer db)
        {
            if (db == null)
                this.db = new LocalSqlServer();
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
        private void Dispose(bool disposing)
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
        #endregion
        #region Contractor
        public IQueryable<pdv_contractors> GetContractors()
        {
            var res = db.pdv_contractors.Where(x => x.isDeleted != true);
            return res;
        }
        public int SaveContractor(pdv_contractors element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_contractors.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        #endregion
        #region Documents
        public IQueryable<pdv_documents> GetDocuments()
        {
            var res = db.pdv_documents
                .Include("pdv_contractors")
                .Include("pdv_documentStatuses")
                .Include("pdv_documentType")
                .Include("pdv_documents2");
            return res;
        }
        public int SaveDocument(pdv_documents element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_documents.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave) 
                Save();
            return element.id;
        }
        public bool DeleteDocument(int id)
        {
            bool res = false;
            var item = db.pdv_documents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        public int SaveDocStatusLog(pdv_documentStatusesLog item)
        {
            if (item.id == 0)
            {
                db.pdv_documentStatusesLog.Add(item);
            }
            else
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return item.id;
        }
        #endregion
        #region Invoices
        public IQueryable<pdv_invoices> GetInvoices()
        {
            var res = db.pdv_invoices
                .Include("pdv_contractors")
                .Include("pdv_documents")
                .Include("pdv_invoiceStatuses");
            return res;
        }
        public int SaveInvoice(pdv_invoices element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_invoices.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = db.pdv_invoices.SingleOrDefault(x => x.id == id);
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
        public IQueryable<pdv_mails> GetMails()
        {
            var res = db.pdv_mails
                .Include("pdv_contractors")
                .Include("pdv_contractors1")
                .Include("pdv_deliveryMailStatuses")
                .Include("pdv_deliveryService");
            return res;
        }
        public int SaveMail(pdv_mails element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_mails.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
             
            if (withSave) Save();
            return element.id;
        }
        public bool DeleteMail(int id)
        {
            bool res = false;
            var item = db.pdv_mails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Delivery Mail Statuses
        public IQueryable<pdv_deliveryMailStatuses> GetDeliveryMailStatuse()
        {
            var res = db.pdv_deliveryMailStatuses;
            return res;
        }
        public int SaveDeliveryMailStatuse(pdv_deliveryMailStatuses element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_deliveryMailStatuses.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteDeliveryMailStatuse(int id)
        {
            bool res = false;
            var item = db.pdv_deliveryMailStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Delivery Service
        public IQueryable<pdv_deliveryService> GetDeliveryService()
        {
            var res = db.pdv_deliveryService;
            return res;
        }
        public int SaveDeliveryService(pdv_deliveryService element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_deliveryService.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteDeliveryService(int id)
        {
            bool res = false;
            var item = db.pdv_deliveryService.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Document Statuses
        public IQueryable<pdv_documentStatuses> GetDocumentStatuses()
        {
            var res = db.pdv_documentStatuses;
            return res;
        }
        public int SaveDocumentStatuses(pdv_documentStatuses element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_documentStatuses.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteDocumentStatus(int id)
        {
            bool res = false;
            var item = db.pdv_documentStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Document Type
        public IQueryable<pdv_documentType> GetDocumentType()
        {
            var res = db.pdv_documentType;
            return res;
        }
        public int SaveDocumentType(pdv_documentType element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_documentType.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteDocumentTypes(int id)
        {
            bool res = false;
            var item = db.pdv_documentType.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Invoice Statuses
        public IQueryable<pdv_invoiceStatuses> GetInvoiceStatuses()
        {
            var res = db.pdv_invoiceStatuses;
            return res;
        }
        public int SaveInvoiceStatuses(pdv_invoiceStatuses element, bool withSave = true)
        {
            if (element.id == 0)
                db.pdv_invoiceStatuses.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        public bool DeleteInvoiceStatus(int id)
        {
            bool res = false;
            var item = db.pdv_invoiceStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion 
    }
}