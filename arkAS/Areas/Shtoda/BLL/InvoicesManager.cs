using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Shtoda.BLL
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

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        #endregion


        #region Invoices

        public List<Shtoda_invoices> GetInvoices()
        {
            var res = new List<Shtoda_invoices>();
            try
            {
                res = _db.GetInvoices().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Shtoda_invoices GetInvoice(int id)
        {
            var res = new Shtoda_invoices();
            try
            {
                res = _db.GetInvoices().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public void SaveInvoice(Shtoda_invoices item)
        {
            try
            {
                _db.SaveInvoice(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
        }

        public bool AddInvoice(int number, Shtoda_invoices user)
        {
            bool res = false;
            var invoice = new Shtoda_invoices();
            try
            {
                invoice = new Shtoda_invoices
                {
                    id = 0,
                    number = number.ToString(),
                    contagentID = user.id,
                    date = DateTime.Now,
                };
                SaveInvoice(invoice);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }

        public bool EditInvoiceField(int id, string code, string value, out string msg, Shtoda_invoices user)
        {
            bool res = false;
            var invoice = new Shtoda_invoices();
            try
            {
                invoice = GetInvoice(id);
                if (invoice.contagentID != user.id) 
                {
                    msg = "Нет прав для редактирования счета";
                    return res;
                }
                if (invoice != null)
                {
                    switch (code)
                    {
                        case "number": invoice.number = value;
                            break;
                        case "contractorID": invoice.contagentID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "comment": invoice.desc = value;
                            break;
                        case "InvoiceStatus": invoice.statusID = RDL.Convert.StrToInt(value, 0);
                            break;
                    }
                    SaveInvoice(invoice);
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

        public bool DeleteInvoice(int id, out string msg, Shtoda_contracts user)
        {
            bool res = false;
            var invoice = new Shtoda_contracts();
            try
            {
                invoice = GetInvoices(id);
                if (invoice != null)
                {
                    if (invoice.contagentID != user.id)
                    {
                        msg = "Нет прав на удаление счета";
                        return res;
                    }
                    //invoice.isDeleted = true; insert in db
                    SaveInvoice(invoice);
                    res = true;
                    msg = "Cчет удален";
                }
                else
                msg = "Не удалось найти счет";
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = invoice.id }, "");
                msg = "Ошибка удаления счета";
            }
            return res;
        }

        #endregion


        #region StatusesInvoice

        public List<Shtoda_statuses_invoces> GetStatusesInvoice()
        {
            var res = new List<Shtoda_statuses_invoces>();
            try
            {
                res = _db.GetStatusesInvoice().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Shtoda_statuses_invoces GetInvoicStatus(int id)
        {
            var res = new Shtoda_statuses_invoces();
            try
            {
                res = _db.GetStatusesInvoice().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public bool AddInvoice(int number, Shtoda_contracts user)
        {
            throw new NotImplementedException();
        }

        public bool EditInvoiceField(int id, string code, string value, out string msg, Shtoda_contracts user)
        {
            throw new NotImplementedException();
        }

        public List<Shtoda_statuses_invoces> GetInvoicesStatuses()
        {
            throw new NotImplementedException();
        }

        public Shtoda_statuses_invoces GetInvoiceStatus(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}