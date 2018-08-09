using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Mikhailova.BLL
{
    public interface IRepository : IDisposable
    {
        #region System
        void Save();
        LocalSqlServer db { get; set; }
        #endregion

        #region Contract
        IQueryable<Mikhailova_contracts> GetContracts();
        int SaveContract(Mikhailova_contracts element, bool withSave = true);
        bool DeleteContract(int id);
        #endregion

        #region Invoices
        IQueryable<Mikhailova_invoices> GetInvoices();
        int SaveInvoice(Mikhailova_invoices element, bool withSave = true);
        #endregion
       
        #region Mails
        IQueryable<Mikhailova_mails> GetMails();
        int SaveMail(Mikhailova_mails element, bool withSave = true);
        #endregion

        #region StatusesContract
        IQueryable<Mikhailova_statuses_contracts> GetStatusesContract();
        int SaveStatusContract(Mikhailova_statuses_contracts element, bool withSave = true);
        bool DeleteStatusContract(int id);
        #endregion

        #region StatusesInvoice
        IQueryable<Mikhailova_statuses_invoces> GetStatusesInvoice();
        int SaveStatusInvoice(Mikhailova_statuses_invoces element, bool withSave = true);
        #endregion

        #region StatusesMail
        IQueryable<Mikhailova_statuses_mails> GetStatusesMail();
        int SaveStatusMail(Mikhailova_statuses_mails element, bool withSave = true);
        #endregion

        #region Contagents
        IQueryable<Mikhailova_contagents> GetContagents();
        int SaveContagent(Mikhailova_contagents element, bool withSave = true);
        bool DeleteContagent(int id);
        #endregion

        #region DocTypes
        IQueryable<Mikhailova_docTypes> GetDocTypes();
        int SaveDocType(Mikhailova_docTypes element, bool withSave = true);
        #endregion

        #region DocTypeTemplates
        IQueryable<Mikhailova_docTypeTemplates> GetDocTypeTemplates();
        int SaveDocTypeTemplate(Mikhailova_docTypeTemplates element, bool withSave = true);
        List<Mikhailova_docTypeTemplates> GetListTemplatesByType(int typeId);
        bool DeleteDocTypeTemplate(int id);
        #endregion

    }
}
