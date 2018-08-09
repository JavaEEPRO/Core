using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        LocalSqlServer db { get; set; }
        void Save();
        #endregion
        #region Contractor
        IQueryable<pdv_contractors> GetContractors();
        int SaveContractor(pdv_contractors element, bool withSave = true);
        #endregion
        #region Documents
        IQueryable<pdv_documents> GetDocuments();
        int SaveDocument(pdv_documents element, bool withSave = true);
        bool DeleteDocument(int id);
        int SaveDocStatusLog(pdv_documentStatusesLog item);
        #endregion
        #region Invoices
        IQueryable<pdv_invoices> GetInvoices();
        int SaveInvoice(pdv_invoices element, bool withSave = true);
        #endregion
        #region Mails
        IQueryable<pdv_mails> GetMails();
        int SaveMail(pdv_mails element, bool withSave = true);
        #endregion
        #region Delivery Mail Statuses
        IQueryable<pdv_deliveryMailStatuses> GetDeliveryMailStatuse();
        int SaveDeliveryMailStatuse(pdv_deliveryMailStatuses element, bool withSave = true);
        bool DeleteDeliveryMailStatuse(int id);
        #endregion
        #region Delivery Service
        IQueryable<pdv_deliveryService> GetDeliveryService();
        int SaveDeliveryService(pdv_deliveryService element, bool withSave = true);
        bool DeleteDeliveryService(int id);
        #endregion
        #region Document Statuses
        IQueryable<pdv_documentStatuses> GetDocumentStatuses();
        bool DeleteDocumentStatus(int id);
        int SaveDocumentStatuses(pdv_documentStatuses element, bool withSave = true);
        #endregion
        #region Document Type
        IQueryable<pdv_documentType> GetDocumentType();
        bool DeleteDocumentTypes(int id);
        int SaveDocumentType(pdv_documentType element, bool withSave = true);
        #endregion
        #region Invoice Statuses
        IQueryable<pdv_invoiceStatuses> GetInvoiceStatuses();
        int SaveInvoiceStatuses(pdv_invoiceStatuses element, bool withSave = true);
        bool DeleteInvoiceStatus(int id);
        #endregion 
    }
}