using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public class Manager : IManager
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
        private IContractorsManager _contractors;
        private IDocumentsManager _documents;
        private IInvoicesManager _invoices;
        private IMailManager _mails;

        public IContractorsManager Contractors
        {
            get
            {
                if (_contractors == null)
                    _contractors = new ContractorsManager(db,(IManager)this);
                return _contractors;
            }
            set
            {
                _contractors = value;
            }
        }
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
        public IInvoicesManager Invoices
        {
            get
            {
                if (_invoices == null)
                    _invoices = new InvoicesManager(db, (IManager)this);
                return _invoices;
            }
            set
            {
                _invoices = value;
            }
        }
        public IMailManager Mails
        {
            get
            {
                if (_mails == null)
                    _mails = new MailManager(db, (IManager)this);
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
                return  db.db.aspnet_Users.FirstOrDefault(x => x.UserName == userName);
            }
            return null;
        }
    }
}