using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public class ContractorsManager : IContractorsManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool disposed;

        public ContractorsManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region Contractors
        public List<udovika_contractor> GetContractors()
        {
            var res = new List<udovika_contractor>();
            try
            {
                res = db.GetContractors().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public List<udovika_contractor> GetContractors2()
        {
            var res = new List<udovika_contractor>();
            try
            {
                res = db.GetContractors2().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public udovika_contractor GetContractor(int id)
        {
            var res = new udovika_contractor();
            try
            {
                res = db.GetContractors().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = id }, "");
            }
            return res;
        }
        #endregion
    }
}