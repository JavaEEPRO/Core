using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Glushko.BLL
{
    public class Manager:IManager
    {
        #region System
        private IRepository db { set; get; }
        public Manager(IRepository db)
        {
            this.db = db;
            _disposed = false;

        }
        private void Save()
        {
            db.Save();
        }
        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private IDocumentsManager _documents;

        public IDocumentsManager Documents
        {
            get
            {
                if (_documents == null)
                    _documents = new DocumentsManager(db, (IManager)this);
                return _documents;
            }
            set
            {
                _documents = value;
            }
        }
    }
}