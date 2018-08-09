using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using System.Data.Entity;

namespace arkAS.Areas.Udovika.BLL
{
    public class Repository : IRepository
    {
        #region System
        private LocalSqlServer _db;
        private bool disposed;
        public LocalSqlServer db
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
            if (db==null)
            {
                this.db = new LocalSqlServer();
            }
            disposed = false;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion
        #region Contractors
        public udovika_contractor GetContractor(int id)
        {
            var res = db.udovika_contractor.FirstOrDefault(x => x.id == id);
            return res;
        }
        #endregion
        #region Documents
        public IQueryable<udovika_contract> GetDocuments()
        {
            var res = db.udovika_contract
                .Include(doc => doc.udovika_contractor)
                .Include(st => st.udovika_statusContract)
                .Include(tp => tp.udovika_contractType);
            return res;
        }
        public udovika_contract GetDocument(int id) 
        {
            var res = db.udovika_contract.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocument(udovika_contract item)
        {
            if (item.id == 0 && item.link != null)
            {
                db.udovika_contract.Add(item);
                Save();
            }
            else 
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Modified; 
                Save();
            }
            return item.id;
        }
        public bool DeleteDocument(int id) 
        {
            bool res = false;
            var item = db.udovika_contract.SingleOrDefault(x => x.id == id);
            if (item!=null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region DocumentsStatus
        public udovika_statusContract GetStatusDocument(int id)
        {
            var res = db.udovika_statusContract.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveSatusDocument(udovika_statusContract status)
        {
            if (status.id == 0)
            {
                db.udovika_statusContract.Add(status);
            }
            else
            {
                db.Entry(status).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return status.id;
        }
        public IQueryable<udovika_statusContract> GetDocumentStatuses()
        {
            var res = db.udovika_statusContract;
            return res;
        }
        #endregion
        #region DocumentType
        public List<udovika_contractType> GetDocumentTypes()
        {
            var res = db.udovika_contractType.Select(x=>x).ToList();
            return res;
        }
        public udovika_contractType GetDocumentType(int id)
        {
            var res = db.udovika_contractType.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveDocumentType(udovika_contractType item)
        {
            if (item.id == 0)
            {
                db.udovika_contractType.Add(item);
            }
            else
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return item.id;
        }
        #endregion
        #region Invoices
        public IQueryable<udovika_invoice> GetInvoices()
        {
            var res = db.udovika_invoice;
            return res;
        }
        public udovika_invoice GetInvoice(int id)
        {
            var res = db.udovika_invoice.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveInvoice(udovika_invoice invoice)
        {
            if (invoice.id == 0)
            {
                db.udovika_invoice.Add(invoice);
                Save();
            }
            else
            {
                db.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return invoice.id;
        }
        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = db.udovika_invoice.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region InvoicesStatus
        public IQueryable<udovika_statusInvoice> GetInvoiceStatuses()
        {
            var res = db.udovika_statusInvoice;
            return res;
        }
        public int SaveStatusInvoice(udovika_statusInvoice status)
        {
            if (status.id == 0)
            {
                db.udovika_statusInvoice.Add(status);
            }
            else
            {
                db.Entry(status).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return status.id;
        }
        public udovika_statusInvoice GetInvoiceStatus(int id)
        {
            var res = db.udovika_statusInvoice.FirstOrDefault(x => x.id == id);
            return res;
        }
        #endregion
        #region Mails
        public IQueryable<udovika_email> GetMails()
        {
            var res = db.udovika_email;
            return res;
        }
        public udovika_email GetMail(int id)
        {
            var res = db.udovika_email.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int SaveMail(udovika_email mail)
        {
            if (mail.id == 0)
            {
                db.udovika_email.Add(mail);
                Save();
            }
            else
            {
                db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return mail.id;
        }
        public bool DeleteMail(int id)
        {
            var res = false;
            var item = db.udovika_email.FirstOrDefault(x => x.id == id);
            if (item != null) 
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                res = true;
                Save();
            }
            return res;
        }
        #endregion
        #region MailsStatus
        public IQueryable<udovika_statusEmail> GetMailStatuses()
        {
            var res = db.udovika_statusEmail;
            return res;
        }
        public udovika_statusEmail GetMailStatus(int id)
        {
            var res = db.udovika_statusEmail.FirstOrDefault(x => x.id == id);
            return res;
        }
        #endregion
        #region Contractors
        public IQueryable<udovika_contractor> GetContractors()
        {
            var res = db.udovika_contractor;
            return res;
        }
        #endregion
        
        public List<udovika_contractor> GetContractors2()
        {
            var res = db.udovika_contractor.ToList();
            return res;
        }
        
        public int SaveDocumentStatusLog(udovika_documentStatusLog item)
        {
            if (item.id == 0)
            {
                db.udovika_documentStatusLog.Add(item);
                Save();
            }
            else
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
            return item.id;
        }
    }
}