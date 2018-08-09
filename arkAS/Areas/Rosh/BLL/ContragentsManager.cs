using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public class ContragentsManager : IContragentsManager
    {
        #region System
        public IReposirory db;
        public IManager mng;
        private bool _disposed;

        public ContragentsManager(IReposirory db, IManager mng)
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

        #region contragents
        public List<rosh_contragents> GetContragents()
        {
            var res = new List<rosh_contragents>();
            try
            {
                res = db.GetContragents().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_contragents GetContragent(int id)
        {
            var res = new rosh_contragents();
            try
            {
                res = db.GetContragent(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveContragent(rosh_contragents res)
        {
            try
            {
                db.SaveContragent(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteContragent(int id)
        {
            try
            {
                db.DeleteContragent(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region contragentSources
        public List<rosh_contragentSources> GetContragentSources()
        {
            var res = new List<rosh_contragentSources>();
            try
            {
                res = db.GetContragentSources().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_contragentSources GetContragentSource(int id)
        {
            var res = new rosh_contragentSources();
            try
            {
                res = db.GetContragentSource(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveContragentSource(rosh_contragentSources res)
        {
            try
            {
                db.SaveContragentSource(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteContragentSource(int id)
        {
            try
            {
                db.DeleteContragentSource(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region contragentStatuses
        public List<rosh_contragentStatuses> GetContragentStatuses()
        {
            var res = new List<rosh_contragentStatuses>();
            try
            {
                res = db.GetContragentStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_contragentStatuses GetContragentStatus(int id)
        {
            var res = new rosh_contragentStatuses();
            try
            {
                res = db.GetContragentStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveContragentStatus(rosh_contragentStatuses res)
        {
            try
            {
                db.SaveContragentStatus(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteContragentStatus(int id)
        {
            try
            {
                db.DeleteContragentStatus(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion
    }
}