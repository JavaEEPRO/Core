using System;
using System.Collections.Generic;
using arkAS.BLL;
using System.Linq;

namespace arkAS.Areas.Tsilurik.BLL
{
    internal class ContractorManager : IContractorManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool _disposed;

        public ContractorManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

            }
            _disposed = true;
        }

        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, "", parameters);
        }
        #endregion

        public List<tsilurik_contractors> GetContractors()
        {
            var res = new List<tsilurik_contractors>();
            try
            {
                res = db.GetContractors().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }
    }
}