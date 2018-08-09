using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Fedchenko.BLL
{
    interface IRepository : IDisposable
    {
        #region System
        void Save();
        #endregion

        #region Document
        IQueryable<fedchenko_documents> GetDocuments();
        int SaveDocument(fedchenko_documents element, bool withSave = true);
        bool DeleteDocument(int id);
        #endregion

        #region DocumentStatus
        IQueryable<fedchenko_docStatus> GetStatusesContract();
        int SaveStatusDocument(fedchenko_docStatus element, bool withSave = true);
        bool DeleteStatusDocument(int id);
        #endregion

        #region DocTypes
        IQueryable<fedchenko_docTypes> GetDocTypes();
        int SaveDocTypes(fedchenko_docTypes element, bool withSave = true);
        #endregion

        #region Invoices
        IQueryable<fedchenko_invoices> GetInvoices();
        int SaveInvoice(fedchenko_invoices element, bool withSave = true);
        #endregion

        #region InvoiceStatus
        IQueryable<fedchenko_invoiceStatus> GetStatusesInvoices();
        int SaveSatausInvoice(fedchenko_invoiceStatus element, bool withSave = true);
        #endregion

        #region Mail
        IQueryable<fedchenko_mail> GetMails();
        int SaveMail(fedchenko_mail element, bool withSave = true);
        #endregion

        #region MailStatus
        IQueryable<fedchenko_mailStatus> GetStatusesMails();
        int SaveStatusMail(fedchenko_mailStatus element, bool withSave = true);
        #endregion

        #region Counterparty
        IQueryable<fedchenko_counterparty> GetCounterparty();
        int SaveCounterparty(fedchenko_counterparty element, bool withSave = true);
        #endregion




    }
}
