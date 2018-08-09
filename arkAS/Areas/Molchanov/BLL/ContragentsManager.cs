using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Areas.Molchanov.Infrastructure;

namespace arkAS.Areas.Molchanov.BLL
{
    public class ContragentsManager : IContragentsManager
    {
        #region System
        private IRepository _db;
        private IManager _mng;
        private bool _disposed;
        public ContragentsManager(IRepository db, IManager mng)
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
        private bool _canAccessToItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            var isManager = user.aspnet_Roles.Any(x => x.RoleName == "guest");
            if (user != null && isAdmin || isManager)
            {
                res = true;
            }
            return res;
        }
        private bool _canManageItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            if (user != null && isAdmin)
            {
                res = true;
            }
            return res;
        }
        private molchanov_logContragents _logContragentChanges(aspnet_Users user, molchanov_contragents contragent, string notice)
        {
            var res = new molchanov_logContragents {date = DateTime.Now, userName = user.UserName, notice = "{contragent.name}: "+notice, contragentID = contragent.id };
            return res;
        }
        #endregion
        #region Contragents
        public List<molchanov_contragents> GetContragents(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_contragents> res;
            try
            {
                if(!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetContragents().ToList();
                }
               
                
            }
            catch(Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }
        public molchanov_contragents GetContragent(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_contragents res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetContragent(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }
        public molchanov_contragents CreateContragent(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_contragents res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_contragents();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "email")
                        {
                            res.email = parameters[key];
                        }
                    }
                    _db.SaveContragent(res);
                    _db.SaveContragentLog(_logContragentChanges(user, res, "Контрагент создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }
        public molchanov_contragents EditContragent(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_contragents res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetContragent(id);
                    foreach(var key in parameters.Keys)
                    {
                        if(key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if(key == "email")
                        {
                            res.email = parameters[key];
                        }
                    }
                    _db.SaveContragent(res);
                    _db.SaveContragentLog(_logContragentChanges(user, res, "Контрагент  изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }
        public bool RemoveContragent(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    msg = "Контрагент удален";
                    _db.DeleteContragent(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        #endregion

    }
}