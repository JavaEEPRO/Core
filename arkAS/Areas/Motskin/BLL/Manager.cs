﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public class Manager: IManager
    {
        #region System
        public Manager(IRepository db)
        {
            this.db = db;
            _disposed = false;
        }

        private IRepository db { set; get; }

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

        private IDocumentsManager _document;
        private IInvoicesManager _invoice;
        private IMailManager _mail;
        private IContractorsManager _contractor;

        public IDocumentsManager Documents
        {
            get
            {
                if (_document == null)
                {
                    _document = new DocumentsManager(db, (IManager)this);
                }
                return _document;
            }
            set
            {
                _document = value;
            }

        }

        public IInvoicesManager Invoices
        {
            get
            {
                if (_invoice == null)
                {
                    _invoice = new InvoicesManager(db, (IManager)this);
                }
                return _invoice;
            }
            set
            {
                _invoice = value;
            }
        }

        public IMailManager Mail
        {
            get
            {
                if (_mail == null)
                {
                    _mail = new MailManager(db, (IManager)this);
                }
                return _mail;
            }
            set
            {
                _mail = value;
            }
        }

        public IContractorsManager Contractors
        {
            get
            {
                if (_contractor == null)
                {
                    _contractor = new ContractorsManager(db, (IManager)this);
                }
                return _contractor;
            }
            set
            {
                _contractor = value;
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