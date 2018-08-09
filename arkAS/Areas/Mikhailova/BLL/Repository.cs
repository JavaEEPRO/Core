using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Mikhailova.BLL
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

        public IQueryable<Mikhailova_contracts> GetContracts()
        {
            var res = db.Mikhailova_contracts.Where(x => x. isDeleted != true);  
            return res;
        }

        public int SaveContract(Mikhailova_contracts element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_contracts.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteContract(int id)
        {
            bool res = false;
            var item = db.Mikhailova_contracts.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Mikhailova_invoices> GetInvoices()
        {
            var res = db.Mikhailova_invoices
                .Include("Mikhailova_contracts")
                .Include("Mikhailova_statuses_invoces");
            return res;
        }

        public int SaveInvoice(Mikhailova_invoices element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_invoices.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteInvoice(int id)
        {
            bool res = false;
            var item = db.Mikhailova_invoices.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Mikhailova_mails> GetMails()
        {
            var res = db.Mikhailova_mails
                .Include("Mikhailova_contracts")
                .Include("Mikhailova_statuses_mails");
            return res;
        }

        public int SaveMail(Mikhailova_mails element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_mails.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;

            if (withSave) Save();
            return element.id;
        }

        public bool DeleteMail(int id)
        {
            bool res = false;
            var item = db.Mikhailova_mails.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Mikhailova_statuses_contracts> GetStatusesContract()
        {
            var res = db.Mikhailova_statuses_contracts;
            return res;
        }

        public int SaveStatusContract(Mikhailova_statuses_contracts element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_statuses_contracts.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteStatusContract(int id)
        {
            bool res = false;
            var item = db.Mikhailova_statuses_contracts.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Mikhailova_statuses_invoces> GetStatusesInvoice()
        {
            var res = db.Mikhailova_statuses_invoces;
            return res;
        }

        public int SaveStatusInvoice(Mikhailova_statuses_invoces element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_statuses_invoces.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        #endregion

        #region StatusesMail

        public IQueryable<Mikhailova_statuses_mails> GetStatusesMail()
        {
            var res = db.Mikhailova_statuses_mails;
            return res;
        }

        public int SaveStatusMail(Mikhailova_statuses_mails element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_statuses_mails.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }
        
        #endregion

        #region Contagents

        public IQueryable<Mikhailova_contagents> GetContagents()
        {
            var res = db.Mikhailova_contagents;
            return res;
        }

        public int SaveContagent(Mikhailova_contagents element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_contagents.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public bool DeleteContagent(int id)
        {
            bool res = false;
            var item = db.Mikhailova_contagents.SingleOrDefault(x => x.id == id);
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

        public IQueryable<Mikhailova_docTypes> GetDocTypes()
        {
            var res = db.Mikhailova_docTypes;
            return res;
        }

        public int SaveDocType(Mikhailova_docTypes element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_docTypes.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        #endregion

        #region DocTypeTemplates

        public IQueryable<Mikhailova_docTypeTemplates> GetDocTypeTemplates()
        {
            var res = db.Mikhailova_docTypeTemplates;
            return res;
        }

        public int SaveDocTypeTemplate(Mikhailova_docTypeTemplates element, bool withSave = true)
        {
            if (element.id == 0)
                db.Mikhailova_docTypeTemplates.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.id;
        }

        public List<Mikhailova_docTypeTemplates> GetListTemplatesByType(int typeId)
        {
            var res = db.Mikhailova_docTypeTemplates.Where(x => x.typeID == typeId).ToList();
            return res;
        }

        public bool DeleteDocTypeTemplate(int id)
        {
            var res = false;
            try
            {
                var item = db.Mikhailova_docTypeTemplates.SingleOrDefault(x => x.id == id);
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