using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Udovika.BLL
{
    public class Manager : IManager
    {
        #region System
        private IRepository db { get; set; }

        private bool disposed = false;
        public Manager(IRepository db)
        {
            this.db = db;
            disposed = false;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private IDocumentsManager document;
        private IContractorsManager contractor;
        private IMailsManager mail;
        private IInvoicesManager invoice;

        public IDocumentsManager Document
        {
            get
            {
                if (document == null)
                    document = new DocumentsManager(db, this);
                return document;
            }
            set
            {
                document = value;
            }
        }
        public IContractorsManager Contractor
        {
            get
            {
                if (contractor == null)
                    contractor = new ContractorsManager(db, this);
                return contractor;
            }
            set
            {
                contractor = value;
            }
        }
        public IInvoicesManager Invoice
        {
            get
            {
                if (invoice == null)
                    invoice = new InvoicesManager(db, this);
                return invoice;
            }
            set
            {
                invoice = value;
            }
        }
        public IMailsManager Mail
        {
            get
            {
                if (mail == null)
                    mail = new MailsManager(db, this);
                return mail;
            }
            set
            {
                mail = value;
            }
        }
        public aspnet_Users GetUser()
        {
            string name = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                name = HttpContext.Current.User.Identity.Name;
                return db.db.aspnet_Users.FirstOrDefault(x => x.UserName == name);
            }
            return null;
        }
    }
}
