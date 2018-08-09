using arkAS.Areas.Molchanov.Infrastructure;
using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.BLL
{
    public class InvoicesManager : IInvoicesManager 
    {
        #region System
        private IRepository _db;
        private IManager _mng;
        private bool _disposed;
        public InvoicesManager(IRepository db, IManager mng)
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
        private molchanov_logInvoices _logInvoicesChanges(aspnet_Users user, molchanov_invoices element, string notice)
        {
            var res = new molchanov_logInvoices {date = DateTime.Now, userName = user.UserName, notice = "{element.number}: " + notice,  invoiceID = element.id };
            return res;
        }
        private molchanov_logInvoiceStatuses _logInvoicesStatusesChanges(aspnet_Users user, molchanov_invoiceStatuses element, string notice)
        {
            var res = new molchanov_logInvoiceStatuses {date = DateTime.Now, userName = user.UserName, notice = "{element.name}: " + notice, invoiceStatusID = element.id };
            return res;
        }

        #endregion
        #region Invoices
        public List<molchanov_invoices> GetInvoices(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_invoices> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetInvoices().Where(x=>x.isDeleted != true).ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_invoices GetInvoice(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_invoices res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetInvoice(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_invoices CreateInvoice(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_invoices res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_invoices();
                    res.uniqueCode = Guid.NewGuid();
                    res.isDeleted = false;
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "number")
                        {
                            res.number = parameters[key];
                        }
                        else if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "invoiceStatus")
                        {
                            res.invStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "contragentName")
                        {
                            res.contragentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                    _db.SaveInvoice(res);
                    _db.SaveInvoiceLog(_logInvoicesChanges(user, res, "Счет создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_invoices EditInvoice(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_invoices res;
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
                    res = _db.GetInvoice(id);
                    foreach(var key in parameters.Keys)
                    {
                        if(key == "number")
                        {
                            res.number = parameters[key];
                        }
                        else if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "invoiceStatus")
                        {
                            res.invStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "contragentName")
                        {
                            res.contragentID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                    _db.SaveInvoice(res);
                    _db.SaveInvoiceLog(_logInvoicesChanges(user, res, "Счет  изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }


        public bool ChangeInvoicesStatus(int id,  string name, string value, out string msg, aspnet_Users user)
        {
            bool res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = false;
                }
                else
                {
                    var item = _db.GetInvoice(id);
                    switch (name)
                    {
                        case "invoiceStatus":
                            int stID = RDL.Convert.StrToInt(value, 0);
                            item.invStatusID = stID;
                            _db.SaveInvoice(item);
                            _db.SaveInvoiceLog(_logInvoicesChanges(user, item, "Документ  изменен"));
                            res = true;
                            break;
                        default: res = false;
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = false;
            }
            return res;
        }

        public bool RemoveInvoice(int id, out string msg, aspnet_Users user)
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
                    _db.GetInvoice(id).isDeleted = true;
                    _db.SaveInvoiceLog(_logInvoicesChanges(user, _db.GetInvoice(id), "Документ помечен на удаление"));
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
        #region InvoiceStatuses
        public List<molchanov_invoiceStatuses> GetInvoiceStatuses(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_invoiceStatuses> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetInvoiceStatuses().ToList();
                }

            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_invoiceStatuses GetInvoiceStatus(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_invoiceStatuses res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetInvoiceStatus(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_invoiceStatuses CreateInvoiceStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_invoiceStatuses res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_invoiceStatuses();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveInvoiceStatus(res);
                    _db.SaveInvoiceStautsLog(_logInvoicesStatusesChanges(user, res, "Статус счета создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_invoiceStatuses EditInvoiceStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_invoiceStatuses res;
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
                    res = _db.GetInvoiceStatus(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveInvoiceStatus(res);
                    _db.SaveInvoiceStautsLog(_logInvoicesStatusesChanges(user, res, "Статус изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }

        public bool RemoveInvoiceStatus(int id, out string msg, aspnet_Users user)
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
                    _db.DeleteInvoice(id);
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