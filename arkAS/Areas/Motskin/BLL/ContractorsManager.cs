using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Models;
using arkAS.BLL.Core;
using Dapper;
using System.Data;
using arkAS.Areas.Motskin.Infrastructura;

namespace arkAS.Areas.Motskin.BLL
{
    public class ContractorsManager : IContractorsManager
    {

        #region System

        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public ContractorsManager(IRepository db, IManager mng)
        {
            _db = db;
            _mng = mng;
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
                    if (_db != null)
                        _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        private bool _IsCanUserChange(aspnet_Users user)
        {
            return (user != null && user.UserName == StaticData.User);
        }

        public bool IsCanCurrentUserChange()
        {
            return _IsCanUserChange(_mng.GetUser());
        }
        #endregion

        #region Contractors
        public List<motskin_contractors> GetAll()
        {
            List <motskin_contractors> res;
            try
            {
                res = _db.GetContractors().ToList();

            }
            catch (Exception ex)
            {
                _debug(ex, null);
                res = null;
            }

            return res;
        }

        public dynamic GetItems(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total)
        {
            msg = "";
            total = 0;
            var name = "";

            if (!_IsCanUserChange(user))
            {
                msg = "Нет прав для данной операции";
                return null;
            }
            try
            {
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    name = parameters.filter.ContainsKey("name") ? parameters.filter["name"].ToString() : "";
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                var rep = new CoreRepository();
                var p = new DynamicParameters();
                p.Add("name", name);
                p.Add("sort1", sort1);
                p.Add("direction1", direction1);
                p.Add("page", parameters.page);
                p.Add("pageSize", parameters.pageSize);
                p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamic res = rep.GetSQLData<dynamic>("motskin_GetContractors", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

                total = p.Get<int>("total");
                return res;
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                msg = "Сбой при выполнении операции";
                return null;
            }
        }

        public motskin_contractors Get(int id, aspnet_Users user, out string msg)
        {
            motskin_contractors res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                res = _db.GetContractors().FirstOrDefault(x => x.id == id);

            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_contractors Create(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_contractors res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }

                res = new motskin_contractors();
                _fillContractor(res, parameters);
                _db.SaveContractor(res);  // сохраняем в бд

            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_contractors Edit(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_contractors res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }

                int id = int.Parse(parameters["id"]);
                res = _db.GetContractors().FirstOrDefault(x => x.id == id);
                if (res != null)
                {
                    _fillContractor(res, parameters);
                    _db.SaveContractor(res);  // сохраняем в бд
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        private void _fillContractor(motskin_contractors item, Dictionary<string, string> parameters)
        {
            foreach (var key in parameters.Keys)
            {
                switch (key)
                {
                    case "name":
                        item.name = parameters[key];
                        break;
                }
            }
        }

        public bool Delete(int id, aspnet_Users user, out string msg)
        {
            bool res = true;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }
                RDL.CacheManager.PurgeCacheItems("motskin_contractors");
                res = _db.DeleteContractor(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }



        #endregion
    }
}