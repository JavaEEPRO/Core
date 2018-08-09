using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public class ContractorManager : IContractorManager
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

        #region Contractor
        private IQueryable<vas_contractors> _getContractors()
        {
            return db.GetContractors().OrderBy(x => x.name);
        }
        private bool _canAccessToContractor(aspnet_Users user, vas_contractors item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_contractors))
            {
                return true;
            }
            return res;
        }
        private bool _canManageContractor(aspnet_Users user, vas_contractors item = null)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_contractors))
            {
                return true;
            }
            return res;
        }
        private string _getRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public vas_contractors GetContractor(int id, aspnet_Users user, out string msg)
        {
            var res = new vas_contractors();
            msg = "";
            try
            {
                res = db.GetContractor(id);
                if (!_canAccessToContractor(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public List<vas_contractors> GetContractors(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg)
        {
            msg = "";
            var res = new List<vas_contractors>();

            if (!_canAccessToContractor(user, null))
            {
                msg = "Нет прав для данной операции";
                return res = null;
            }

            try
            {
                var items = _getContractors();
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    var name = parameters.filter.ContainsKey("name") ? parameters.filter["name"].ToString() : "";

                    if (name != "")
                    {
                        items = items.ToList().Where(x => x.name != null && x.name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                if (direction1 == "up") items = items.OrderBy(x => x.name);
                else items = items.OrderByDescending(x => x.name);

                res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public List<vas_contractors> GetContractors()
        {
            var user = mng.GetUser();
            if (!_canAccessToContractor(user, null))
            {
                return new List<vas_contractors>();
            }

            return db.GetContractors().ToList();
        }
        public vas_contractors CreateContractor(string name, aspnet_Users user, out string msg)
        {
            var res = new vas_contractors();
            msg = "";
            try
            {
                if (!_canManageContractor(user))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }

                res.name = name;
                res.isDeleted = false;
                res.code = _getRandomString(8).ToLower();

                db.SaveContractor(res);

            }
            catch (Exception ex)
            {
                _debug(ex, new { name = name, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }
        public bool DeleteContractor(int id, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetContractor(id);
                if (!_canManageContractor(user, item))
                {
                    msg = "У вас нет прав на данную операцию";
                    return res;
                }
                if (!db.DeleteContractor(id))
                {
                    throw new Exception("Сбой при удалении записи с базы");
                }

                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = id, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }
        public bool EditContractorField(int pk, string name, string value, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetContractor(pk);
                if (!_canManageContractor(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                item.name = value;

                db.SaveContractor(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = pk, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }

        #endregion
    }
}