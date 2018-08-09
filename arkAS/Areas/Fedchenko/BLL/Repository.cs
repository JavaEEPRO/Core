using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using System.Web.Mvc;
using System.Data.Entity;

namespace arkAS.Areas.Fedchenko.BLL
{
    public class Repository:IRepository
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

        #region Documents
        public IQueryable<fedchenko_documents> GetDocuments()
        {
            var res = db.fedchenko_documents.Where(x => x.fedchenko_docStatus != true);
            return res;
        }
        public int SaveDocument(fedchenko_documents element, bool withSave = true)
        {
            if (element.Id == 0)
                db.fedchenko_documents.Add(element);
            else
                db.Entry(element).State = EntityState.Modified;
            if (withSave)
                Save();
            return element.Id;
        }

        public bool DeleteDocument (int id)
        {
            bool res = false;
            var item = db.fedchenko_documents.SingleOrDefault(x => x.Id == id);
            if (item !=null)
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