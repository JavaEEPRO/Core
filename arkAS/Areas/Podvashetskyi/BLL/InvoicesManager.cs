using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
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
            if(!_disposed)
            {
                if(disposing)
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
        public List<pdv_invoices> GetInvoices()
        {
            var res = new List<pdv_invoices>();
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
        public pdv_invoices GetInvoice(int id)
        {
            var res = new pdv_invoices();
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
        public void SaveInvoice(pdv_invoices item)
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
        public bool AddInvoice(int number, pdv_contractors user)
        {
            bool res = false;
            var invoice = new pdv_invoices();
            try
            {
                invoice = new pdv_invoices
                {
                    id = 0,
                    number = number,
                    contractorID = user.id,
                    createDate = DateTime.Now,
                };
                SaveInvoice(invoice);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }
        public bool EditInvoiceField(int id, string code, string value, out string msg, pdv_contractors user)
        {
            bool res = false;
            var invoice = new pdv_invoices();
            try
            {
                invoice = GetInvoice(id);
                if(invoice.contractorID != user.id)
                {
                    msg = "Нет прав для редактирования счета";
                    return res;
                }
                if (invoice != null)
                {
                    switch (code)
                    {
                        case "number": invoice.number = RDL.Convert.StrToInt(value, 0);
                            break;
                        // case "contractorID": invoice.contractorID = RDL.Convert.StrToInt(value, 0);
                        //    break;
                        case "comment": invoice.comment = value;
                            break;
                        case "documentID": invoice.documentID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "InvoiceStatus": invoice.invoiceStatusID = RDL.Convert.StrToInt(value, 0);
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
        public bool DeleteInvoice(int id, out string msg, pdv_contractors user)
        {
            bool res = false;
            var invoice = new pdv_invoices();
            try
            {
                invoice = GetInvoice(id);
                if (invoice != null)
                {
                    if (invoice.contractorID != user.id)
                    {
                        msg = "Нет прав на удаление счета";
                        return res;
                    }
                    invoice.isDeleted = true;
                    SaveInvoice(invoice);
                    res = true;
                    msg = "Cчет удален";
                }
                else
                    msg = "Ну удалось найти счет";
            }
            catch(Exception ex)
            {
                _debug(ex, new { id = invoice.id }, "");
                msg = "Ошибка удаления счета";
            }
            return res;
        }
        #endregion 
        #region Invoices Statuses 
        public List<pdv_invoiceStatuses> GetInvoicesStatuses()
        {
            var res = new List<pdv_invoiceStatuses>();
            try
            {
                res = _db.GetInvoiceStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_invoiceStatuses GetInvoicesStatus(int id)
        {
            var res = new pdv_invoiceStatuses();
            try
            {
                res = _db.GetInvoiceStatuses().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveInvoicesStatus(pdv_invoiceStatuses item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveInvoiceStatuses(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }
        public bool DeleteInvoiceStatus(int id)
        {
            var res = false;
            try
            {
                res = _db.DeleteInvoiceStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        public bool EditInvoiceStatusField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var invoiceStatus = new pdv_invoiceStatuses();
            try
            {
                invoiceStatus = GetInvoicesStatus(id);

                if (invoiceStatus != null)
                {
                    switch (code)
                    {
                        case "name": invoiceStatus.name = value;
                            break;
                        case "code": invoiceStatus.code = value;
                            break;

                    }
                    SaveInvoicesStatus(invoiceStatus);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти статус документ";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        #endregion 
    }
}
