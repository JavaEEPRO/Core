using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        LocalSqlServer db { get; set; }
        void Save();
        #endregion
        #region Contragents
        IQueryable<molchanov_contragents> GetContragents();
        molchanov_contragents GetContragent(int id);
        int SaveContragent(molchanov_contragents element, bool withSave = true);
        bool DeleteContragent(int id);
        #endregion
        #region Documents
        IQueryable<molchanov_documents> GetDocuments();
        molchanov_documents GetDocument(int id);
        int SaveDocument(molchanov_documents element, bool withSave = true);
        bool DeleteDocument(int id);
        #endregion
        #region DocTypes
        IQueryable<molchanov_docTypes> GetDocTypes();
        molchanov_docTypes GetDocType(int id);
        int SaveDocType(molchanov_docTypes element, bool withSave = true);
        bool DeleteDocType(int id);
        #endregion
        #region DocStatuses
        IQueryable<molchanov_docStatuses> GetDocStatuses();
        molchanov_docStatuses GetDocStatus(int id);
        int SaveDocStatus(molchanov_docStatuses element, bool withSave = true);
        bool DeleteDocStatus(int id);
        #endregion
        #region Invoices
        IQueryable<molchanov_invoices> GetInvoices();
        molchanov_invoices GetInvoice(int id);
        int SaveInvoice(molchanov_invoices element, bool withSave = true);
        bool DeleteInvoice(int id);
        #endregion
        #region InvoiceStatuses
        IQueryable<molchanov_invoiceStatuses> GetInvoiceStatuses();
        molchanov_invoiceStatuses GetInvoiceStatus(int id);
        int SaveInvoiceStatus(molchanov_invoiceStatuses element, bool withSave = true);
        bool DeleteInvoiceStatus(int id);
        #endregion
        #region Mails
        IQueryable<molchanov_mails> GetMails();
        molchanov_mails GetMail(int id);
        int SaveMail(molchanov_mails element, bool withSave = true);
        bool DeleteMail(int id);
        #endregion
        #region MailStatuses
        IQueryable<molchanov_mailStatuses> GetMailStatuses();
        molchanov_mailStatuses GetMailStatus(int id);
        int SaveMailStatus(molchanov_mailStatuses element, bool withSave = true);
        bool DeleteMailStatus(int id);
        #endregion
        #region DeliverySystems 
        IQueryable<molchanov_deliverySystems> GetDeliverySystems();
        molchanov_deliverySystems GetDeliverySystem(int id);
        int SaveDeliverySystem(molchanov_deliverySystems element, bool withSave = true);
        bool DeleteDeliverySystem(int id);
        #endregion
        #region LogContragents
        IQueryable<molchanov_logContragents> GetContragentLogs();
        molchanov_logContragents GetContragentLog(int id);
        int SaveContragentLog(molchanov_logContragents element, bool withSave = true);
        bool DeleteContragentLog(int id);
        #endregion
        #region LogDocuments
        IQueryable<molchanov_logDocuments> GetDocumentLogs();
        molchanov_logDocuments GetDocumentLog(int id);
        int SaveDocumentLog(molchanov_logDocuments element, bool withSave = true);
        bool DeleteDocumentLog(int id);
        #endregion
        #region LogDocTypes
        IQueryable<molchanov_logDocTypes> GetDocTypesLogs();
        molchanov_logDocTypes GetDocTypeLog(int id);
        int SaveDocTypeLog(molchanov_logDocTypes element, bool withSave = true);
        bool DeleteDocTypeLog(int id);
        #endregion
        #region LogDocStatuses
        IQueryable<molchanov_logDocStatuses> GetDocStatusesLogs();
        molchanov_logDocStatuses GetDocStatusesLog(int id);
        int SaveDocStausesLog(molchanov_logDocStatuses element, bool withSave = true);
        bool DeleteDocStatusesLog(int id);
        #endregion
        #region LogInvoices
        IQueryable<molchanov_logInvoices> GetInvoiceLogs();
        molchanov_logInvoices GetInvoiceLog(int id);
        int SaveInvoiceLog(molchanov_logInvoices element, bool withSave = true);
        bool DeleteInvoiceLog(int id);
        #endregion
        #region LogInvoiceStatuses
        IQueryable<molchanov_logInvoiceStatuses> GetInvoiceStautsLog();
        molchanov_logInvoiceStatuses GetInvoiceStatusLog(int id);
        int SaveInvoiceStautsLog(molchanov_logInvoiceStatuses element, bool withSave = true);
        bool DeleteInvoiceStatusLog(int id);
        #endregion
        #region LogMails
        IQueryable<molchanov_logMails> GetMailLogs();
        molchanov_logMails GetMailLog(int id);
        int SaveMailLog(molchanov_logMails element, bool withSave = true);
        bool DeleteMailLog(int id);
        #endregion
        #region LogMailStatuses
        IQueryable<molchanov_logMailStatuses> GetMailStatusLogs();
        molchanov_logMailStatuses GetMailStatusLog(int id);
        int SaveMailStatusLog(molchanov_logMailStatuses element, bool withSave = true);
        bool DeleteMailStatusLog(int id);
        #endregion
        #region LogDeliverySystems 
        IQueryable<molchanov_logDeliverySystems> GetDeliverySystemLogs();
        molchanov_logDeliverySystems GetDeliverySystemLog(int id);
        int SaveDeliverySystemLog(molchanov_logDeliverySystems element, bool withSave = true);
        bool DeleteDeliverySystemLog(int id);
        #endregion
    }
}