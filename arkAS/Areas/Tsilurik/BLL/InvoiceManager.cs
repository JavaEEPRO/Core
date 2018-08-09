using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace arkAS.Areas.Tsilurik.BLL
{
    internal class InvoiceManager : IInvoiceManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool _disposed;

        public InvoiceManager(IRepository db, IManager mng)
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

        #region Invoice
        private IQueryable<tsilurik_invoices> _getInvoices()
        {
            return db.GetInvoices().OrderBy(x => x.date);
        }
        private bool _canAccessToInvoice(aspnet_Users user, tsilurik_invoices item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is tsilurik_invoices))
            {
                return true;
            }
            return res;
        }
        private bool _canManageInvoice(aspnet_Users user, tsilurik_invoices item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is tsilurik_invoices))
            {
                return true;
            }
            return res;
        }
        public bool _logInvoiceStatuses(tsilurik_invoices item, string note = "")
        {
            var res = false;
            try
            {
                db.SaveLogsInvoiceStatus(new tsilurik_statusLog
                {

                    created = DateTime.Now,
                    statusID = item.statusID,
                    invoiceID = item.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = item.id, statusID = item.statusID, note });
            }
            return res;
        }
        public List<tsilurik_invoices> GetInvoices(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg,out int total)
        {
            msg = "";
            total = 0;
            var res = new List<tsilurik_invoices>();

            if (!_canAccessToInvoice(user, null))
            {
                msg = "Нет прав для данной операции";
                return res = null;
            }

            try
            {
                var items = _getInvoices();
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    var number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                    var contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                    List<int?> statusIDs = new List<int?>();
                    if (parameters.filter.ContainsKey("statusIDs"))
                    {
                        statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    }

                    items = items.Where(x => 
                        (contractorID == 0 || x.contractorID == contractorID) &&
                        (statusIDs.Count == 0 || statusIDs.Contains(x.statusID))
                        );

                    if (number != "")
                    {
                        items = items.ToList().Where(x => x.number != null && x.number.IndexOf(number, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                switch (sort1)
                {
                    case "number":
                        if (direction1 == "up") items = items.OrderBy(x => x.number);
                        else items = items.OrderByDescending(x => x.number);
                        break;
                    case "date":
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                    case "status":
                        if (direction1 == "up") items = items.OrderBy(x => x.tsilurik_status.name);
                        else items = items.OrderByDescending(x => x.tsilurik_status.name);
                        break;
                    case "contractor":
                        if (direction1 == "up") items = items.OrderBy(x => x.tsilurik_contractors.name);
                        else items = items.OrderByDescending(x => x.tsilurik_contractors.name);
                        break;
                    default:
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                }
                total = items.Count();
                res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_invoices GetInvoice(int id, aspnet_Users user, out string msg)
        {
            var res = new tsilurik_invoices();
            msg = "";
            try
            {
                res = db.GetInvoice(id);
                if (!_canAccessToInvoice(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_invoices CreateInvoice(Dictionary<string, object> parameters, aspnet_Users user, out string msg)
        {
            var res = new tsilurik_invoices();
            msg = "";
            try
            {
                if (!_canManageInvoice(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                if (RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0) == 0)
                {
                    var date = DateTime.Now;
                    var number = AjaxModel.GetValueFromSaveField("number", fields);
                    var contractorID = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("contractor", fields), 0);
                    var statusID = GetInvoiceStatuses().FirstOrDefault(x => x.name == "создан").id;
                    var note = AjaxModel.GetValueFromSaveField("note", fields);
                    var item = new tsilurik_invoices { number = number, date = date, contractorID = contractorID, statusID = statusID, note = note};
                    db.SaveInvoice(item);
                    res = item;
                }
                else
                {
                    res = _editInvoice(fields, out msg);
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
        public bool ChangeInvoiceStatus(int id, string statusID, string name, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetInvoice(id);
                if (!_canManageInvoice(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                switch (name)
                {
                    case "status":
                        if (statusID != "") item.statusID = RDL.Convert.StrToInt(statusID, 0);
                        _logInvoiceStatuses(item, "Статус изменен " + user.UserName);
                        break;
                }
                db.SaveInvoice(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_invoices _editInvoice(List<Dictionary<string, object>> fields, out string msg)
        {
            var res = new tsilurik_invoices();
            msg = "";
            try
            {
                var id = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0);
                var item = db.GetInvoice(id);

                item.number = AjaxModel.GetValueFromSaveField("number", fields);
                item.contractorID = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("contractor", fields), 0);
                item.note = AjaxModel.GetValueFromSaveField("note", fields);
                db.SaveInvoice(item);
                res = item;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = res.id });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public List<tsilurik_status> GetInvoiceStatuses()
        {
            var res = new List<tsilurik_status>();
            try
            {
                res = db.GetStatuses("invoice").ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }
            return res;
        }

        #endregion
    }
}