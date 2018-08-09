using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public class Manager: IManager
    {
        #region System
        public IReposirory db { get; set; }
        private bool _disposed;

        public Manager(IReposirory db)
        {
            this.db = db;
            _disposed = false;
        }

        private void Save()
        {
            db.Save();
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
                    if (db != null)
                        db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }
        #endregion

        private IContragentsManager _contragents;
        public IContragentsManager Contragents
        {
            get
            {
                if (_contragents == null)
                {
                    _contragents = new ContragentsManager(db, (IManager)this);
                }
                return _contragents;
            }
            set
            {
                _contragents = value;
            }
        }

        private IOrderManager _orders;
        public IOrderManager Orders
        {
            get
            {
                if (_orders == null)
                {
                    _orders = new OrderManager(db, (IManager)this);
                }
                return _orders;
            }
            set
            {
                _orders = value;
            }
        }

        private IDocumentsManager _documents;
        public IDocumentsManager Documents
        {
            get
            {
                if (_documents == null)
                {
                    _documents = new DocumentsManager(db, (IManager)this);
                }
                return _documents;
            }
            set
            {
                _documents = value;
            }
        }

        private IMailsManager _mails;
        public IMailsManager Mails
        {
            get
            {
                if (_mails == null)
                {
                    _mails = new MailsManager(db, (IManager)this);
                }
                return _mails;
            }
            set
            {
                _mails = value;
            }
        }

        public aspnet_Users GetUser()
        {
            string userName = "";

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                userName = HttpContext.Current.User.Identity.Name;

                return db.db.aspnet_Users.FirstOrDefault(x => x.UserName == userName);
            }
            return null;
        }
    }
}