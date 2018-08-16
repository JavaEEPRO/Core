using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Shtoda.BLL
{
    public class Repository : IRepository
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

        public Repository()
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
    
        #region Contracts

        public IQueryable<Shtoda_contracts> GetContracts()
        {
            var res = db.Shtoda_contracts.Where(x => x. isDeleted != true);  
            return res;
        }

        public int SaveContract(Shtoda_contracts element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_contracts.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteContract(int id)
        {
            bool res = false;
            var item = db.Shtoda_contracts.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        #endregion

        #region Invoices

        public IQueryable<Shtoda_invoices> GetInvoices()
        {
            var res = db.Shtoda_invoices
                .Include("Shtoda_contracts")
                .Include("Shtoda_statuses_invoces");
            return res;
        }

        public int SaveInvoice(Shtoda_invoices element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_invoices.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = db.Shtoda_invoices.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Shtoda_mails> GetMails()
        {
            var res = db.Shtoda_mails
                .Include("Shtoda_contracts")
                .Include("Shtoda_statuses_mails");
            return res;
        }

        public int SaveMail(Shtoda_mails element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_mails.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;

            if (withSave) Save();
            return element.id;
        }

        public bool DeleteMail(int id)
        {
            bool res = false;
            var item = db.Shtoda_mails.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        #endregion

        #region StatusesContract

        public IQueryable<Shtoda_statuses_contracts> GetStatusesContract()
        {
            var res = db.Shtoda_statuses_contracts;
            return res;
        }

        public int SaveStatusContract(Shtoda_statuses_contracts element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_statuses_contracts.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteStatusContract(int id)
        {
            bool res = false;
            var item = db.Shtoda_statuses_contracts.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        #endregion

        #region StatusesInvoice

        public IQueryable<Shtoda_statuses_invoces> GetStatusesInvoice()
        {
            var res = db.Shtoda_statuses_invoces;
            return res;
        }

        public int SaveStatusInvoice(Shtoda_statuses_invoces element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_statuses_invoces.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        #endregion

        #region StatusesMail

        public IQueryable<Shtoda_statuses_mails> GetStatusesMail()
        {
            var res = db.Shtoda_statuses_mails;
            return res;
        }

        public int SaveStatusMail(Shtoda_statuses_mails element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_statuses_mails.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        
        #endregion

        #region Contragents

        public IQueryable<Shtoda_contragents> GetContragents()
        {
            var res = db.Shtoda_contragents;
            return res;
        }

        public int SaveContagent(Shtoda_contragents element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_contragents.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteContragent(int id)
        {
            bool res = false;
            var item = db.Shtoda_contragents.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        #endregion

        #region DocTypes

        public IQueryable<Shtoda_docTypes> GetDocTypes()
        {
            var res = db.Shtoda_docTypes;
            return res;
        }

        public int SaveDocType(Shtoda_docTypes element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_docTypes.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        #endregion

        #region DocTypeTemplates

        public IQueryable<Shtoda_docTypeTemplates> GetDocTypeTemplates()
        {
            var res = db.Shtoda_docTypeTemplates;
            return res;
        }

        public int SaveDocTypeTemplate(Shtoda_docTypeTemplates element, bool withSave = true)
        {
            if (element.id == 0)
                db.Shtoda_docTypeTemplates.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public List<Shtoda_docTypeTemplates> GetListTemplatesByType(int typeId)
        {
            var res = db.Shtoda_docTypeTemplates.Where(x => x.typeID == typeId).ToList();
            return res;
        }

        public bool DeleteDocTypeTemplate(int id)
        {
            var res = false;
            try
            {
                var item = db.Shtoda_docTypeTemplates.SingleOrDefault(x => x.id == id);
                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }


        #endregion

    }
}