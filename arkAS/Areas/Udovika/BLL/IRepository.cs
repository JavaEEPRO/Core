using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        void Save();
        LocalSqlServer db { get; set; }
        #endregion
        #region Contractors
        IQueryable<udovika_contractor> GetContractors();
        List<udovika_contractor> GetContractors2();
        udovika_contractor GetContractor(int id);
        #endregion

        #region Documents
        IQueryable<udovika_contract> GetDocuments();
        udovika_contract GetDocument(int id);
        int SaveDocument(udovika_contract item);
        bool DeleteDocument(int id);
        #endregion
        #region Status
        int SaveSatusDocument(udovika_statusContract status);
        int SaveDocumentStatusLog(udovika_documentStatusLog item);
        IQueryable<udovika_statusContract> GetDocumentStatuses();
        #endregion
        #region Type
        List<udovika_contractType> GetDocumentTypes();
        udovika_contractType GetDocumentType(int id);
        int SaveDocumentType(udovika_contractType item);
        #endregion

        #region Invoices
        IQueryable<udovika_invoice> GetInvoices();
        udovika_invoice GetInvoice(int id);
        int SaveInvoice(udovika_invoice invoice);
        bool DeleteInvoice(int id);
        #endregion
        #region Status
        IQueryable<udovika_statusInvoice> GetInvoiceStatuses();
        int SaveStatusInvoice(udovika_statusInvoice status);
        udovika_statusInvoice GetInvoiceStatus(int id);
        #endregion

        #region Mails
        IQueryable<udovika_email> GetMails();
        udovika_email GetMail(int id);
        int SaveMail(udovika_email mail);
        bool DeleteMail(int id);
        #endregion
        #region Status
        IQueryable<udovika_statusEmail> GetMailStatuses();
        udovika_statusEmail GetMailStatus(int id);
        #endregion
    }
}
