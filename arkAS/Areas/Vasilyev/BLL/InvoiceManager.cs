using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public class InvoiceManager : IInvoiceManager
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
        private IQueryable<vas_invoices> _getInvoices()
        {
            return db.GetInvoices().OrderBy(x => x.date);
        }
        private bool _canAccessToInvoice(aspnet_Users user, vas_invoices item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_invoices))
            {
                return true;
            }
            return res;
        }
        private bool _canManageInvoice(aspnet_Users user, vas_invoices item = null)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_invoices))
            {
                return true;
            }
            return res;
        }
        public bool _logInvoiceStatusChange(vas_invoices item, string note = "")
        {
            var res = false;
            try
            {
                db.SaveInvoiceStatusLog(new vas_invoiceStatusesLog
                    {
                        id = 0,
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
        private string _getRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public vas_invoices GetInvoice(int id, aspnet_Users user, out string msg)
        {
            var res = new vas_invoices();
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
                _debug(ex, new { invoiceID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }

        public List<vas_invoices> GetInvoices(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg)
        {
            msg = "";
            var res = new List<vas_invoices>();

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
                    var contractorID = parameters.filter.ContainsKey("contractorID") ? RDL.Convert.StrToInt(parameters.filter["contractorID"].ToString(), 0) : 0;
                    List<int?> statusIDs = new List<int?>();
                    if (parameters.filter.ContainsKey("statusIDs"))
                    {
                        statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    }
                    DateTime createdMin = DateTime.MinValue, createdMax = DateTime.MaxValue;
                    if (parameters.filter.ContainsKey("date") && parameters.filter["date"] != null)
                    {
                        var dates = parameters.filter["date"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dates.Length > 0)
                        {
                            createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), DateTime.MinValue);
                        }
                        if (dates.Length > 1)
                        {
                            createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), DateTime.MaxValue);
                        }
                    }

                    items = items.Where(x => (contractorID == 0 || x.contractorID == contractorID) &&
                        (statusIDs.Count == 0 || statusIDs.Contains(x.statusID)) &&
                        (createdMin <= x.date && x.date <= createdMax)
                        );
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
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_invoiceStatuses.name);
                        else items = items.OrderByDescending(x => x.vas_invoiceStatuses.name);
                        break;
                    case "contractor":
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_contractors.name);
                        else items = items.OrderByDescending(x => x.vas_contractors.name);
                        break;
                    default:
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                }
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

        public vas_invoices CreateInvoice(int contractorID, aspnet_Users user, out string msg)
        {
            var res = new vas_invoices();
            msg = "";
            try
            {
                if (!_canManageInvoice(user))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }

                res.number = _getRandomString(8) ;
                res.date = DateTime.Now;
                res.statusID = 1;
                res.contractorID = contractorID;
                res.comment = "Комментарий";
                res.code = _getRandomString(8).ToLower();

                db.SaveInvoice(res);

            }
            catch (Exception ex)
            {
                _debug(ex, new { contractorID = contractorID, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }

        public bool EditInvoiceField(int pk, string name, string value, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetInvoice(pk);
                if (!_canManageInvoice(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                switch (name)
                {
                    case "number": item.number = value; break;
                    case "comment": item.comment = value; break;
                    case "contractor": if (value != "") item.contractorID = RDL.Convert.StrToInt(value, 0); break;
                    case "status": if (value != "") item.statusID = RDL.Convert.StrToInt(value, 0);
                        _logInvoiceStatusChange(item, "Статус изменен " + user.UserName);
                        break;
                }
                db.SaveInvoice(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = pk, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }

        public bool DeleteInvoice(int id, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetInvoice(id);
                if (!_canManageInvoice(user, item))
                {
                    msg = "У вас нет прав на данную операцию";
                    return res;
                }
                if (!db.DeleteInvoice(id))
                {
                    throw new Exception("Сбой при удалении записи с базы");
                }

                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceID = id, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }
        #endregion

        #region Invoice Status
        public vas_invoiceStatuses GetInvoiceStatus(int id)
        {
            var res = new vas_invoiceStatuses();

            try
            {
                res = db.GetInvoiceStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }

            return res;
        }

        public List<vas_invoiceStatuses> GetInvoiceStatuses()
        {
            var res = new List<vas_invoiceStatuses>();

            try
            {
                res = db.GetInvoiceStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
            }
            return res;
        }

        public vas_invoiceStatuses CreateInvoiceStatus(string name, string code)
        {
            var res = new vas_invoiceStatuses() { name = name, code = code };

            try
            {
                db.SaveInvoiceStatus(res);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }

            return res;
        }

        public void DeleteInvoiceStatus(int id)
        {
            try
            {
                db.DeleteInvoiceStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }
        }

        #endregion
    }
}