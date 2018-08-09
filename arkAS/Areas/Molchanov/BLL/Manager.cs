using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Areas.Molchanov.Infrastructure;

namespace arkAS.Areas.Molchanov.BLL
{
    public class Manager : IManager
    {
        #region System
        private IRepository db;
        private bool _disposed;
        public Manager (IRepository db)
        {
            this.db = db;
            _disposed = false;
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

                    if (db != null) db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }
        #endregion
        IContragentsManager _contragents;
        IDocumentsManager _documents;
        IInvoicesManager _invoices;
        IMailsManager _mails;
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

        public IDocumentsManager Documents
        {
            get
            {
               if(_documents == null)
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

        public IInvoicesManager Invoices
        {
            get
            {
                if(_invoices == null)
                {
                    _invoices = new InvoicesManager(db, (IManager)this);
                }
                return _invoices;
            }

            set
            {
                _invoices = value;
            }
        }

        public IMailsManager Mails
        {
            get
            {
                if (_mails == null)
                {
                    _mails = new MailsManager(db, (IManager)this);
                }
                return  _mails;
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