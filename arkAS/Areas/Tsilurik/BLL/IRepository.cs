using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        LocalSqlServer Db { get; set; }
        void Save();
        #endregion

        #region Document
        tsilurik_documents GetDocument(int id);
        IQueryable<tsilurik_documents> GetDocuments();
        int SaveDocument(tsilurik_documents item, bool withSave = true);
        int UpdateDocumentStatus(int id, int statusID, bool withSave = true);
        IQueryable<tsilurik_types> GetDocumentTypes();
        int SaveLogsDocumentStatus(tsilurik_statusLog item);
        #endregion

        #region Invoices
        tsilurik_invoices GetInvoice(int id);
        IQueryable<tsilurik_invoices> GetInvoices();
        int SaveInvoice(tsilurik_invoices item, bool withSave = true);
        int UpdateInvoiceStatus(int id, int statusID, bool withSave = true);
        int SaveLogsInvoiceStatus(tsilurik_statusLog item);
        #endregion

        #region Mails
        tsilurik_mails GetMail(int id);
        IQueryable<tsilurik_mails> GetMails();
        int SaveMail(tsilurik_mails item, bool withSave = true);
        int UpdateMailStatus(int id, int statusID, bool withSave = true);
        int SaveLogsMailStatus(tsilurik_statusLog item);
        #endregion

        #region Contractors
        IQueryable<tsilurik_contractors> GetContractors();
        #endregion

        #region Statuses
        IQueryable<tsilurik_status> GetStatuses(string document);
        #endregion

    }
}
