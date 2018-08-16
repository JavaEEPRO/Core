using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        void Save();
        LocalSqlServer db { get; set; }
        #endregion

        #region Contract
        IQueryable<Shtoda_contracts> GetContracts();
        int SaveContract(Shtoda_contracts element, bool withSave = true);
        bool DeleteContract(int id);
        #endregion

        #region Invoices
        IQueryable<Shtoda_invoices> GetInvoices();
        int SaveInvoice(Shtoda_invoices element, bool withSave = true);
        #endregion
       
        #region Mails
        IQueryable<Shtoda_mails> GetMails();
        int SaveMail(Shtoda_mails element, bool withSave = true);
        #endregion

        #region StatusesContract
        IQueryable<Shtoda_statuses_contracts> GetStatusesContract();
        int SaveStatusContract(Shtoda_statuses_contracts element, bool withSave = true);
        bool DeleteStatusContract(int id);
        #endregion

        #region StatusesInvoice
        IQueryable<Shtoda_statuses_invoces> GetStatusesInvoice();
        int SaveStatusInvoice(Shtoda_statuses_invoces element, bool withSave = true);
        #endregion

        #region StatusesMail
        IQueryable<Shtoda_statuses_mails> GetStatusesMail();
        int SaveStatusMail(Shtoda_statuses_mails element, bool withSave = true);
        #endregion

        #region Contagents
        IQueryable<Shtoda_contagents> GetContagents();
        int SaveContagent(Shtoda_contagents element, bool withSave = true);
        bool DeleteContagent(int id);
        #endregion

        #region DocTypes
        IQueryable<Shtoda_docTypes> GetDocTypes();
        int SaveDocType(Shtoda_docTypes element, bool withSave = true);
        #endregion

        #region DocTypeTemplates
        IQueryable<Shtoda_docTypeTemplates> GetDocTypeTemplates();
        int SaveDocTypeTemplate(Shtoda_docTypeTemplates element, bool withSave = true);
        List<Shtoda_docTypeTemplates> GetListTemplatesByType(int typeId);
        bool DeleteDocTypeTemplate(int id);
        #endregion

    }
}
