using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Mikhailova.BLL
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

        private IContractsManager _contracts;
        private IInvoicesManager _invoices;
        private IMailsManager _mails;

        public IContractsManager Contracts
        {
            get
            {
                if (_contracts == null)
                    _contracts = new ContractsManager(db, (IManager)this);
                return _contracts;
            }
            set
            {
                _contracts = value;
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

        public IMailsManager Mails
        {
            get
            {
                if (_mails == null)
                    _mails = new MailsManager(db, (IManager)this);
                return _mails;
            }
            set
            {
                _mails = value;
            }
        }

        IInvoicesManager IManager.Invoices
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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