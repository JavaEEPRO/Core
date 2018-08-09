using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public class MailsManager: IMailsManager
    {
        #region System
        public IReposirory db;
        public IManager mng;
        private bool _disposed;

        public MailsManager(IReposirory db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            //this.Debug = debug;
            _disposed = false;
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
                    if (db != null)
                        db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion

        #region mails
        public List<rosh_mails> GetMails()
        {
            var res = new List<rosh_mails>();
            try
            {
                res = db.GetMails().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_mails GetMail(int id)
        {
            var res = new rosh_mails();
            try
            {
                res = db.GetMail(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveMail(rosh_mails res)
        {
            try
            {
                db.SaveMail(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteMail(int id)
        {
            try
            {
                db.DeleteMail(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region mailStatuses
        public List<rosh_mailStatuses> GetMailStatuses()
        {
            var res = new List<rosh_mailStatuses>();
            try
            {
                res = db.GetMailStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_mailStatuses GetMailStatus(int id)
        {
            var res = new rosh_mailStatuses();
            try
            {
                res = db.GetMailStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveMailStatus(rosh_mailStatuses res)
        {
            try
            {
                db.SaveMailStatus(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteMailStatus(int id)
        {
            try
            {
                db.DeleteMailStatus(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region mailLogs
        public List<rosh_mailLogs> GetMailLogs()
        {
            var res = new List<rosh_mailLogs>();
            try
            {
                res = db.GetMailLogs().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_mailLogs GetMailLog(int id)
        {
            var res = new rosh_mailLogs();
            try
            {
                res = db.GetMailLog(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveMailLog(rosh_mailLogs res)
        {
            try
            {
                db.SaveMailLog(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteMailLog(int id)
        {
            try
            {
                db.DeleteMailLog(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region sendingSystems
        public List<rosh_sendingSystems> GetSendingSystems()
        {
            var res = new List<rosh_sendingSystems>();
            try
            {
                res = db.GetSendingSystems().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_sendingSystems GetSandingSystem(int id)
        {
            var res = new rosh_sendingSystems();
            try
            {
                res = db.GetSandingSystem(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveSandingSystem(rosh_sendingSystems res)
        {
            try
            {
                db.SaveSandingSystem(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteSandingSystem(int id)
        {
            try
            {
                db.DeleteSandingSystem(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion
    }
}