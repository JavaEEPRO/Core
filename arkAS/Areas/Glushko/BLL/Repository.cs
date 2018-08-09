using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Glushko.BLL
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

        public IQueryable<gl_docs> GetDocuments()
        {
            var res = db.gl_docs.Include(x => x.gl_docTypes).Include(x => x.gl_docStatuses);
            return res;
        }

        public bool DeleteDocument(int id)
        {
            bool res = false;
            var item = db.gl_docs.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        public int SaveDocument(gl_docs element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.gl_docs.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        #endregion

        #region Doc Types
        public IQueryable<gl_docTypes> GetDocumentTypes()
        {
            var res = db.gl_docTypes;
            return res;
        }

        public bool DeleteDocumentType(int id)
        {
            bool res = false;
            var item = db.gl_docTypes.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        public int SaveDocumentType(gl_docTypes element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.gl_docTypes.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }
        #endregion

        #region Doc Statuses
        public bool DeleteDocumentStatus(int id)
        {
            bool res = false;
            var item = db.gl_docStatuses.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                Save();
                res = true;
            }
            return res;
        }

        public int SaveDocumentStatus(gl_docStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                db.gl_docStatuses.Add(element);
                if (withSave) Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) Save();
            }
            return element.id;
        }

        public IQueryable<gl_docStatuses> GetDocumentStatuses()
        {
            var res = db.gl_docStatuses;
            return res;
        }
        #endregion


        public IQueryable<fin_contragents> GetFinContragents()
        {
            IQueryable<fin_contragents> res = db.fin_contragents;
            return res;
        }
    }
}