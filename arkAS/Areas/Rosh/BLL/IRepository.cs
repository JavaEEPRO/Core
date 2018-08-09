using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;


namespace arkAS.Areas.Rosh.BLL
{
    public interface IReposirory : IDisposable
    {
        #region System
        void Save();
        LocalSqlServer db { get; set; }
        #endregion

        #region contragents
        IQueryable<rosh_contragents> GetContragents();
        rosh_contragents GetContragent(int id);
        int SaveContragent(rosh_contragents contragent);
        bool DeleteContragent(int id);
        #endregion

        #region contragentSources
        IQueryable<rosh_contragentSources> GetContragentSources();
        rosh_contragentSources GetContragentSource(int id);
        int SaveContragentSource(rosh_contragentSources source);
        bool DeleteContragentSource(int id);
        #endregion

        #region contragentStatuses
        IQueryable<rosh_contragentStatuses> GetContragentStatuses();
        rosh_contragentStatuses GetContragentStatus(int id);
        int SaveContragentStatus(rosh_contragentStatuses status);
        bool DeleteContragentStatus(int id);
        #endregion

        #region orders
        IQueryable<rosh_orders> GetOrders();
        rosh_orders GetOrder(int id);
        int SaveOreder(rosh_orders oreder);
        bool DeleteOrder(int id);
        #endregion

        #region orderStatuses
        IQueryable<rosh_orderStatuses> GetOrderStatuses();
        rosh_orderStatuses GetOrderStatus(int id);
        int SaveOrederStatus(rosh_orderStatuses orederStatus);
        bool DeleteOrderStatus(int id);
        #endregion

        #region orderLogs
        IQueryable<rosh_orderLogs> GetOrderLogs();
        rosh_orderLogs GetOrderLog(int id);
        int SaveOrederLog(rosh_orderLogs orederLog);
        bool DeleteOrderLog(int id);        
        #endregion

        #region documents
        IQueryable<rosh_documents> GetDocuments();
        rosh_documents GetDocument(int id);
        int SaveDocument(rosh_documents document);
        bool DeleteDocument(int id);
        #endregion

        #region docTypes
        IQueryable<rosh_docTypes> GetDocTypes();
        rosh_docTypes GetDocType(int id);
        int SaveDocType(rosh_docTypes docType);
        bool DeleteDocType(int id);
        #endregion

        #region docStatuses
        IQueryable<rosh_docStatuses> GetDocStatuses();
        rosh_docStatuses GetDocStatus(int id);
        int SaveDocStatus(rosh_docStatuses docStatus);
        bool DeleteDocStatus(int id);
        #endregion

        #region docLogs
        IQueryable<rosh_docLogs> GetDocLogs();
        rosh_docLogs GetDocLog(int id);
        int SaveDocLog(rosh_docLogs docLog);
        bool DeleteDocLog(int id);
        #endregion

        #region mails
        IQueryable<rosh_mails> GetMails();
        rosh_mails GetMail(int id);
        int SaveMail(rosh_mails mail);
        bool DeleteMail(int id);
        #endregion

        #region mailStatuses
        IQueryable<rosh_mailStatuses> GetMailStatuses();
        rosh_mailStatuses GetMailStatus(int id);
        int SaveMailStatus(rosh_mailStatuses mailStatus);
        bool DeleteMailStatus(int id);
        #endregion

        #region mailLogs
        IQueryable<rosh_mailLogs> GetMailLogs();
        rosh_mailLogs GetMailLog(int id);
        int SaveMailLog(rosh_mailLogs mailLog);
        bool DeleteMailLog(int id);
        #endregion

        #region sendingSystems
        IQueryable<rosh_sendingSystems> GetSendingSystems();
        rosh_sendingSystems GetSandingSystem(int id);
        int SaveSandingSystem(rosh_sendingSystems sandingSystem);
        bool DeleteSandingSystem(int id);
        #endregion
    }
}
