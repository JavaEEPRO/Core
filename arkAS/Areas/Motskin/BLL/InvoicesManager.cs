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
    public class InvoicesManager: IInvoicesManager
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

        private bool _IsCanUserChange(aspnet_Users user)
        {
            return (user != null && user.UserName == StaticData.User);
        }

        public bool IsCanCurrentUserChange()
        {
            return _IsCanUserChange(_mng.GetUser());
        }
        #endregion

        #region Invoices

        public dynamic GetItems(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total)
        {
            msg = "";
            total = 0;
            var number = "";
            var strStatusIDs = "";
            var contractorID = "";
            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (!_IsCanUserChange(user))
            {
                msg = "Нет прав для данной операции";
                return null;
            }
            try
            {
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                    contractorID = parameters.filter.ContainsKey("contractorID") ? parameters.filter["contractorID"].ToString() : "";
                    strStatusIDs = parameters.filter.ContainsKey("strStatusIDs") ? parameters.filter["strStatusIDs"].ToString() : "";
                    if (parameters.filter.ContainsKey("created") && parameters.filter["created"] != null)
                    {
                        var dates = parameters.filter["created"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dates.Length > 0)
                        {
                            createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                        }
                        if (dates.Length > 1)
                        {
                            createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                            createdMax = createdMax.AddDays(1).AddSeconds(-1);
                        }
                    }
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                var rep = new CoreRepository();
                var p = new DynamicParameters();
                p.Add("number", number);
                p.Add("strStatusIDs", strStatusIDs);
                p.Add("contractorID", contractorID);
                p.Add("createdMin", createdMin);
                p.Add("createdMax", createdMax);
                p.Add("sort1", sort1);
                p.Add("direction1", direction1);
                p.Add("page", parameters.page);
                p.Add("pageSize", parameters.pageSize);
                p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamic res = rep.GetSQLData<dynamic>("motskin_GetInvoices", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

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

        public motskin_invoices Get(int id, aspnet_Users user, out string msg)
        {
            motskin_invoices res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                res = _db.GetInvoices().FirstOrDefault(x => x.id == id);

            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_invoices Create(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_invoices res=null;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                
                var statusID = GetStatuses().FirstOrDefault(x => x.code == "created").id;
                Guid guid = Guid.NewGuid();
                res = new motskin_invoices
                {
                    date = DateTime.Now,        // дата неизменна
                    invoiceStatusID = statusID,
                    createdUnique = guid
                };
                _fillInvoice(res, parameters);

                _db.SaveInvoice(res);  // сохраняем в бд
                // и сразу же получаем из базы этот объект, чтобы узнать его id.
                motskin_invoices item = _db.GetInvoices().FirstOrDefault(x => x.createdUnique == guid);
                _logChangeStatus(item, "Статус изменен " + user.UserName);
            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_invoices Edit(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_invoices res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                int id = int.Parse(parameters["id"]);
                res = _db.GetInvoices().FirstOrDefault(x => x.id == id);
                if (res != null)
                {
                    _fillInvoice(res, parameters);

                    _db.SaveInvoice(res);  // сохраняем в бд
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

        private void _fillInvoice(motskin_invoices item, Dictionary<string, string> parameters)
        {
            foreach (var key in parameters.Keys)
            {
                switch (key)
                {
                    case "number":
                        item.number = parameters[key];
                        break;
                    case "contractorName":
                        item.contractorID = RDL.Convert.StrToInt(parameters[key], 0);
                        break;
                    case "comment":
                        item.comment = parameters[key];
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
                RDL.CacheManager.PurgeCacheItems("motskin_invoices");
                res = _db.DeleteInvoice(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        #endregion

        #region Status
        public bool ChangeStatus(int id, string value, string parametrName, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                var item = _db.GetInvoices().FirstOrDefault(x => x.id == id);

                switch (parametrName)
                {
                    case "invoiceStatusName":
                        int statusID = 0;
                        if (value != "")
                            statusID = RDL.Convert.StrToInt(value, 0);
                        if (item.invoiceStatusID != statusID)  // проверка, если этот метод вызовет контроллер напрямую
                        {
                            item.invoiceStatusID = statusID;
                            _logChangeStatus(item, "Статус изменен " + user.UserName);
                            _db.SaveInvoice(item);
                        }
                        res = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public List<motskin_invoiceStatuses> GetStatuses()
        {
            var res = new List<motskin_invoiceStatuses>();
            try
            {
                res = _db.GetInvoiceStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }
            return res;
        }
        #endregion

        #region LogStatus
        public List<motskin_invoiceStatusLog> GetLogStatus(int id)
        {
            var res = new List<motskin_invoiceStatusLog>();
            try
            {
                res = _db.GetInvoiceStatusLog().Where(x => x.invoiceID == id).ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }

        private bool _logChangeStatus(motskin_invoices item, string note = "")
        {
            var res = false;
            try
            {
                _db.SaveInvoiceStatusLog(new motskin_invoiceStatusLog
                {

                    created = DateTime.Now,
                    invoiceStatusID = item.invoiceStatusID,
                    invoiceID = item.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = item.id, statusID = item.invoiceStatusID, note });
            }
            return res;
        }

        #endregion
    }
}