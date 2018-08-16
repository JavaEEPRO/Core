using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Shtoda.BLL
{     
    public class AddonsManager : IAddonsManager
    {
        #region System
        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public AddonsManager(IRepository db, IManager mng)
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

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        #endregion


        #region Addons

        public List<Shtoda_addons> GetAddons()
        {
            var res = new List<Shtoda_addons>();
            try
            {
                res = _db.GetAddons().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Shtoda_addons GetAddon(int id)
        {
            var res = new Shtoda_addons();
            try
            {
                res = _db.GetAddons().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public void SaveAddon(Shtoda_addons item)
        {
            try
            {
                _db.SaveAddon(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
        }

        public bool AddAddon(int number, Shtoda_addons user)
        {
            bool res = false;
            var addon = new Shtoda_addons();
            try
            {
                addon = new Shtoda_addons
                {
                    id = 0,
                    number = number.ToString(),
                    contagentID = user.id,
                    date = DateTime.Now,
                };
                Saveaddon(addon);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }

        public bool EditAddonField(int id, string code, string value, out string msg, Shtoda_addons user)
        {
            bool res = false;
            var addon = new Shtoda_addons();
            try
            {
                addon = GetAddon(id);
                if (addon.contagentID != user.id) 
                {
                    msg = "Нет прав для редактирования счета";
                    return res;
                }
                if (addon != null)
                {
                    switch (code)
                    {
                        case "number": addon.number = value;
                            break;
                        case "contractorID": addon.contagentID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "comment": addon.desc = value;
                            break;
                        case "addonStatus": addon.statusID = RDL.Convert.StrToInt(value, 0);
                            break;
                    }
                    SaveAddon(addon);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти счет";
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
                msg = "Ошибка редатирования";
            }
            return res;
        }

        public bool DeleteAddon(int id, out string msg, Shtoda_contracts user)
        {
            bool res = false;
            var addon = new Shtoda_contracts();
            try
            {
                addon = GetAddons(id);
                if (addon != null)
                {
                    if (addon.contagentID != user.id)
                    {
                        msg = "Нет прав на удаление счета";
                        return res;
                    }
                    addon.isDeleted = true; insert in db
                    Saveaddon(addon);
                    res = true;
                    msg = "Cчет удален";
                }
                else
                msg = "Не удалось найти счет";
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = addon.id }, "");
                msg = "Ошибка удаления счета";
            }
            return res;
        }

        #endregion


        #region StatusesAddon

        public List<Shtoda_statuses_addons> GetStatusesAddon()
        {
            var res = new List<Shtoda_statuses_addons>();
            try
            {
                res = _db.GetStatusesAddon().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Shtoda_statuses_addons GetAddonStatus(int id)
        {
            var res = new Shtoda_statuses_addons();
            try
            {
                res = _db.GetStatusesAddon().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public bool AddAddon(int number, Shtoda_contracts user)
        {
            throw new NotImplementedException();
        }

        public bool EditAddonField(int id, string code, string value, out string msg, Shtoda_contracts user)
        {
            throw new NotImplementedException();
        }

        public List<Shtoda_statuses_addons> GetAddonStatuses()
        {
            throw new NotImplementedException();
        }

        public Shtoda_statuses_addons GetAddonStatus(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}