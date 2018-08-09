using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace arkAS.Areas.Rosh.BLL
{
    public class Repository: IReposirory
    {
        #region System
        private LocalSqlServer _db;
        public LocalSqlServer db
        {
            get
            {
                if (_db == null)
                {
                    _db = new LocalSqlServer();
                }
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        private bool _disposed;

        public Repository(LocalSqlServer db)
        {
            if (db == null) this.db = new LocalSqlServer();
            _disposed = false;
        }
        //public Repository()
        //{
        //    db = new LocalSqlServer();
        //    _disposed = false;
        //}

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (db != null) db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }
        #endregion

        #region contragents
        public IQueryable<rosh_contragents> GetContragents()
        {
            var contragents = db.rosh_contragents;
            return contragents;
        }

        public rosh_contragents GetContragent(int id)
        {
            var contragent = new rosh_contragents();
            contragent = db.rosh_contragents.FirstOrDefault(c => c.id == id);
            return contragent;
        }

        public int SaveContragent(rosh_contragents contragent)
        {
            try
            {
                if (contragent.id == 0)
                {
                    db.rosh_contragents.Add(contragent);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(contragent).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return contragent.id;
        }

        public bool DeleteContragent(int id)
        {
            bool contragent = false;
            try
            {
                var item = db.rosh_contragents.SingleOrDefault(c => c.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                contragent = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return contragent;
        }
        #endregion

        #region contragentSources
        public IQueryable<rosh_contragentSources> GetContragentSources()
        {
            var sources = db.rosh_contragentSources;
            return sources;
        }

        public rosh_contragentSources GetContragentSource(int id)
        {
            var source = new rosh_contragentSources();
            source = db.rosh_contragentSources.FirstOrDefault(c => c.id == id);
            return source;
        }

        public int SaveContragentSource(rosh_contragentSources source)
        {
            try
            {
                if (source.id == 0)
                {
                    db.rosh_contragentSources.Add(source);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(source).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return source.id;
        }

        public bool DeleteContragentSource(int id)
        {
            bool source = false;
            try
            {
                var item = db.rosh_contragentSources.SingleOrDefault(c => c.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                source = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return source;
        }
        #endregion

        #region contragentStatuses
        public IQueryable<rosh_contragentStatuses> GetContragentStatuses()
        {
            var statuses = db.rosh_contragentStatuses;
            return statuses;
        }

        public rosh_contragentStatuses GetContragentStatus(int id)
        {
            var status = new rosh_contragentStatuses();
            status = db.rosh_contragentStatuses.FirstOrDefault(c => c.id == id);
            return status;
        }

        public int SaveContragentStatus(rosh_contragentStatuses status)
        {
            try
            {
                if (status.id == 0)
                {
                    db.rosh_contragentStatuses.Add(status);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(status).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return status.id;
        }

        public bool DeleteContragentStatus(int id)
        {
            bool status = false;
            try
            {
                var item = db.rosh_contragentStatuses.SingleOrDefault(c => c.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                status = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return status;
        }
        #endregion

        #region orders
        public IQueryable<rosh_orders> GetOrders()
        {
            var orders = db.rosh_orders;
            return orders;
        }

        public rosh_orders GetOrder(int id)
        {
            //var order = new rosh_orders();
            //order = db.rosh_orders.FirstOrDefault(o => o.id == id);
            var order = db.rosh_orders.FirstOrDefault(x => x.id == id);
            return order;
        }

        public int SaveOreder(rosh_orders order)
        {
            try
            {
                if (order.id == 0)
                {
                    db.rosh_orders.Add(order);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return order.id;
        }

        public bool DeleteOrder(int id)
        {
            bool order = false;
            try
            {
                var item = db.rosh_orders.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                order = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return order;
        }
        #endregion

        #region orderStatuses
        public IQueryable<rosh_orderStatuses> GetOrderStatuses()
        {
            var orderStatuses = db.rosh_orderStatuses;
            return orderStatuses;
        }

        public rosh_orderStatuses GetOrderStatus(int id)
        {
            var orderStatus = new rosh_orderStatuses();
            orderStatus = db.rosh_orderStatuses.FirstOrDefault(o => o.id == id);
            return orderStatus;
        }

        public int SaveOrederStatus(rosh_orderStatuses orederStatus)
        {
            try
            {
                if (orederStatus.id == 0)
                {
                    db.rosh_orderStatuses.Add(orederStatus);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(orederStatus).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return orederStatus.id;
        }

        public bool DeleteOrderStatus(int id)
        {
            bool orderStatus = false;
            try
            {
                var item = db.rosh_orderStatuses.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                orderStatus = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return orderStatus;
        }
        #endregion

        #region orderLogs
        public IQueryable<rosh_orderLogs> GetOrderLogs()
        {
            var orderLogs = db.rosh_orderLogs;
            return orderLogs;
        }

        public rosh_orderLogs GetOrderLog(int id)
        {
            var orderLog = new rosh_orderLogs();
            orderLog = db.rosh_orderLogs.FirstOrDefault(o => o.id == id);
            return orderLog;
        }

        public int SaveOrederLog(rosh_orderLogs orederLog)
        {
            try
            {
                if (orederLog.id == 0)
                {
                    db.rosh_orderLogs.Add(orederLog);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(orederLog).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return orederLog.id;
        }

        public bool DeleteOrderLog(int id)
        {
            bool orederLog = false;
            try
            {
                var item = db.rosh_orderLogs.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                orederLog = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return orederLog;
        }
        #endregion

        #region documents
        public IQueryable<rosh_documents> GetDocuments()
        {
            var res = db.rosh_documents;
            return res;
        }

        public rosh_documents GetDocument(int id)
        {
            var res = new rosh_documents();
            res = db.rosh_documents.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveDocument(rosh_documents res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_documents.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteDocument(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_documents.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion

        #region docTypes
        public IQueryable<rosh_docTypes> GetDocTypes()
        {
            var res = db.rosh_docTypes;
            return res;
        }

        public rosh_docTypes GetDocType(int id)
        {
            var res = new rosh_docTypes();
            res = db.rosh_docTypes.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveDocType(rosh_docTypes res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_docTypes.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteDocType(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_docTypes.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion

        #region docStatuses
        public IQueryable<rosh_docStatuses> GetDocStatuses()
        {
            var res = db.rosh_docStatuses;
            return res;
        }

        public rosh_docStatuses GetDocStatus(int id)
        {
            var res = new rosh_docStatuses();
            res = db.rosh_docStatuses.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveDocStatus(rosh_docStatuses res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_docStatuses.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteDocStatus(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_docStatuses.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion

        #region docLogs
        public IQueryable<rosh_docLogs> GetDocLogs()
        {
            var docLogs = db.rosh_docLogs;
            return docLogs;
        }

        public rosh_docLogs GetDocLog(int id)
        {
            var docLog = new rosh_docLogs();
            docLog = db.rosh_docLogs.FirstOrDefault(o => o.id == id);
            return docLog;
        }

        public int SaveDocLog(rosh_docLogs docLog)
        {
            try
            {
                if (docLog.id == 0)
                {
                    db.rosh_docLogs.Add(docLog);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(docLog).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return docLog.id;
        }

        public bool DeleteDocLog(int id)
        {
            bool docLog = false;
            try
            {
                var item = db.rosh_docLogs.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                docLog = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return docLog;
        }
        #endregion

        #region mails
        public IQueryable<rosh_mails> GetMails()
        {
            var res = db.rosh_mails;
            return res;
        }

        public rosh_mails GetMail(int id)
        {
            var res = new rosh_mails();
            res = db.rosh_mails.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveMail(rosh_mails res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_mails.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteMail(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_mails.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion

        #region mailStatuses
        public IQueryable<rosh_mailStatuses> GetMailStatuses()
        {
            var res = db.rosh_mailStatuses;
            return res;
        }

        public rosh_mailStatuses GetMailStatus(int id)
        {
            var res = new rosh_mailStatuses();
            res = db.rosh_mailStatuses.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveMailStatus(rosh_mailStatuses res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_mailStatuses.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteMailStatus(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_mailStatuses.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion

        #region mailLogs
        public IQueryable<rosh_mailLogs> GetMailLogs()
        {
            var mailLogs = db.rosh_mailLogs;
            return mailLogs;
        }

        public rosh_mailLogs GetMailLog(int id)
        {
            var mailLog = new rosh_mailLogs();
            mailLog = db.rosh_mailLogs.FirstOrDefault(o => o.id == id);
            return mailLog;
        }

        public int SaveMailLog(rosh_mailLogs mailLog)
        {
            try
            {
                if (mailLog.id == 0)
                {
                    db.rosh_mailLogs.Add(mailLog);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(mailLog).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return mailLog.id;
        }

        public bool DeleteMailLog(int id)
        {
            bool mailLog = false;
            try
            {
                var item = db.rosh_mailLogs.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                mailLog = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return mailLog;
        }
        #endregion

        #region sendingSystems
        public IQueryable<rosh_sendingSystems> GetSendingSystems()
        {
            var res = db.rosh_sendingSystems;
            return res;
        }

        public rosh_sendingSystems GetSandingSystem(int id)
        {
            var res = new rosh_sendingSystems();
            res = db.rosh_sendingSystems.FirstOrDefault(o => o.id == id);
            return res;
        }

        public int SaveSandingSystem(rosh_sendingSystems res)
        {
            try
            {
                if (res.id == 0)
                {
                    db.rosh_sendingSystems.Add(res);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        db.Entry(res).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        RDL.Debug.LogError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res.id;
        }

        public bool DeleteSandingSystem(int id)
        {
            bool res = false;
            try
            {
                var item = db.rosh_sendingSystems.SingleOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                res = true;
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return res;
        }
        #endregion
    }
}