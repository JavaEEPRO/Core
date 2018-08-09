using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public class ContractorsManager : IContractorsManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool _disposed;

        public ContractorsManager(IRepository db, IManager mng)
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

        #region Contractors
        private bool _canAccessToContractor(aspnet_Users user)
        {
            var res = false;
            if(user != null && user.UserName == "core-admin@mail.ru")
            {
                res = true;
            }
            return res;
        }
        public List<pdv_contractors> GetContractors(aspnet_Users user, out string msg)
        {
            msg = "";
            var res = new List<pdv_contractors>();
            try
            {
                if (!_canAccessToContractor(user))
                {
                    msg = "Недостаточна прав для просмотра!";
                    return null;
                }
                res = db.GetContractors().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Возникла ошибка!";
            }
            return res;
        }
        public List<pdv_contractors> GetContractors()
        {
            var user = mng.GetUser();
            if (!_canAccessToContractor(user))
            {
                return new List<pdv_contractors>();
            }

            return db.GetContractors().ToList();
        }
        public pdv_contractors GetContractor(int id)
        {
            var res = new pdv_contractors();
            try
            {
                res = db.GetContractors().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = id }, "");
            }
            return res;
        }
        public void SaveContractor(pdv_contractors item)
        {
            try
            {
                db.SaveContractor(item);
            }
            catch (Exception ex)
            {
                _debug(ex, item, "");
            }
        }
        public bool CreateContractor(string name)
        {
            var res = false;
            var contractor = new pdv_contractors();
            try
            {
                contractor = new pdv_contractors { id = 0, name = name };
                SaveContractor(contractor);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, contractor, "");
            }
            return res;
        }
        public bool EditContractorsField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var contractor = new pdv_contractors();
            try
            {
                contractor = GetContractor(id);

                if (contractor != null)
                {
                    switch (code)
                    {
                        case "name": contractor.name = value;
                            break;
                    }
                    SaveContractor(contractor);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти контрагента";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        public bool DeleteContractor(int id, out string msg)
        {
            bool res = false;
            var contractor = new pdv_contractors();
            try
            {
                contractor = GetContractor(id);
                contractor.isDeleted = true;
                SaveContractor(contractor);
                msg = "Контрагент удален";
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorId = id }, "");
                msg = "Не удалось удалить контрагента";
            }
            return res;
        }
        #endregion
    }
}