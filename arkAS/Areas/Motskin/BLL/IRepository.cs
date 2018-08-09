using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IRepository: IDisposable
    {
        #region System
        void Save();
        LocalSqlServer db { get; set; }
        #endregion

        #region Documents
        IQueryable<motskin_documents> GetDocuments();
        motskin_documents GetDocument(int id);
        int SaveDocument(motskin_documents element, bool withSave = true);
        bool DeleteDocument(int id);
        #endregion

        #region Invoices
        IQueryable<motskin_invoices> GetInvoices();
        motskin_invoices GetInvoice(int id);
        int SaveInvoice(motskin_invoices element, bool withSave = true);
        bool DeleteInvoice(int id);
        #endregion

        #region Mail
        IQueryable<motskin_mails> GetMails();
        motskin_mails GetMail(int id);
        int SaveMail(motskin_mails element, bool withSave = true);
        bool DeleteMail(int id);
        #endregion

        #region DocumentStatuses
        IQueryable<motskin_documentStatuses> GetDocumentStatuses();
        motskin_documentStatuses GetDocumentStatuse(int id);
        int SaveDocumentStatus(motskin_documentStatuses element, bool withSave = true);
        bool DeleteDocumentStatus(int id);
        #endregion

        #region InvoiceStatuses
        IQueryable<motskin_invoiceStatuses> GetInvoiceStatuses();
        motskin_invoiceStatuses GetInvoiceStatuse(int id);
        int SaveInvoiceStatus(motskin_invoiceStatuses element, bool withSave = true);
        bool DeleteInvoiceStatus(int id);
        #endregion

        #region MailStatuses
        IQueryable<motskin_mailStatuses> GetMailStatuses();
        motskin_mailStatuses GetMailStatuse(int id);
        int SaveMailStatus(motskin_mailStatuses element, bool withSave = true);
        bool DeleteMailStatus(int id);
        #endregion

        #region Contractors
        IQueryable<motskin_contractors> GetContractors();
        motskin_contractors GetContractor(int id);
        int SaveContractor(motskin_contractors element, bool withSave = true);
        bool DeleteContractor(int id);
        #endregion

        #region DocumentTypes
        IQueryable<motskin_documentTypes> GetDocumentTypes();
        motskin_documentTypes GetDocumentType(int id);
        int SaveDocumentType(motskin_documentTypes element, bool withSave = true);
        bool DeleteDocumentType(int id);
        #endregion

        #region SendSystems
        IQueryable<motskin_sendSystems> GetSendSystems();
        motskin_sendSystems GetSendSystem(int id);
        int SaveSendSystem(motskin_sendSystems element, bool withSave = true);
        bool DeleteSendSystem(int id);
        #endregion

        #region DocumentStatusLog
        IQueryable<motskin_documentStatusLog> GetDocumentStatusLog();
        int SaveDocumentStatusLog(motskin_documentStatusLog element, bool withSave = true);
        #endregion

        #region InvoiceStatusLog
        IQueryable<motskin_invoiceStatusLog> GetInvoiceStatusLog();
        int SaveInvoiceStatusLog(motskin_invoiceStatusLog element, bool withSave = true);
        #endregion

        #region MailStatusLog
        IQueryable<motskin_mailStatusLog> GetMailStatusLog();
        int SaveMailStatusLog(motskin_mailStatusLog element, bool withSave = true);
        #endregion
    }
}
