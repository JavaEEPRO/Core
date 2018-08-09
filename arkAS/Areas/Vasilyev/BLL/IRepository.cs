using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        LocalSqlServer Db { get; set; }
        void Save();
        #endregion

        #region Document
        vas_documents GetDocument(int id);
        IQueryable<vas_documents> GetDocuments();
        int SaveDocument(vas_documents item, bool withSave = true);
        bool DeleteDocument(int id);
        int SaveDocStatusLog(vas_docStatusesLog item);
        #endregion

        #region Invoices
        vas_invoices GetInvoice(int id);
        IQueryable<vas_invoices> GetInvoices();
        int SaveInvoice(vas_invoices item, bool withSave = true);
        bool DeleteInvoice(int id);
        int SaveInvoiceStatusLog(vas_invoiceStatusesLog item);
        #endregion

        #region Mails
        vas_mails GetMail(int id);
        IQueryable<vas_mails> GetMails();
        int SaveMail(vas_mails item, bool withSave = true);
        bool DeleteMail(int id);
        int SaveMailStatusLog(vas_mailStatusesLog item);
        #endregion

        #region Contractors
        vas_contractors GetContractor(int id);
        IQueryable<vas_contractors> GetContractors();
        int SaveContractor(vas_contractors item, bool withSave = true);
        bool DeleteContractor(int id);
        #endregion

        #region Document Statuses
        vas_docStatuses GetDocStatus(int id);
        IQueryable<vas_docStatuses> GetDocStatuses();
        int SaveDocStatus(vas_docStatuses item, bool withSave = true);
        bool DeleteDocStatus(int id);
        #endregion

        #region Document Types
        vas_docTypes GetDocType(int id);
        IQueryable<vas_docTypes> GetDocTypes();
        int SaveDocType(vas_docTypes item, bool withSave = true);
        bool DeleteDocType(int id);
        #endregion

        #region Invoice Statuses
        vas_invoiceStatuses GetInvoiceStatus(int id);
        IQueryable<vas_invoiceStatuses> GetInvoiceStatuses();
        int SaveInvoiceStatus(vas_invoiceStatuses item, bool withSave = true);
        bool DeleteInvoiceStatus(int id);
        #endregion

        #region Mail Statuses
        vas_mailStatuses GetMailStatus(int id);
        IQueryable<vas_mailStatuses> GetMailStatuses();
        int SaveMailStatus(vas_mailStatuses item, bool withSave = true);
        bool DeleteMailStatus(int id);
        #endregion
    }
}