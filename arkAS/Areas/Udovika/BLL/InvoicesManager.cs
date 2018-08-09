using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public class InvoicesManager : IInvoicesManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool disposed;

        public InvoicesManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region Invoice
        public bool GetPermissionAccessInvoice(aspnet_Users user)
        {
            var res = false;
            if (user != null && user.UserName == "touch-test@gmail.com")
            {
                return true;
            }
            return res;
        }
        public List<udovika_invoice> GetInvoices()
        {
            var res = new List<udovika_invoice>();
            try
            {
                res = db.GetInvoices().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public udovika_invoice GetInvoice(int id, aspnet_Users user, out string msg)
        {
            var item = new udovika_invoice();
            msg = "";
            try
            {
                if (!GetPermissionAccessInvoice(user))
                {
                    msg = "Недостаточно прав.";
                    return item = null;
                }
                item = GetInvoices().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { cinvoiceID = id }, "");
            }
            return item;
        }
        public bool CreateInvoice(string name, aspnet_Users user, out string msg)
        {
            bool res = false;
            var invoice = new udovika_invoice();
            msg = "";
            try
            {
                if (!GetPermissionAccessInvoice(user))
                {
                    msg = "Недостаточно прав.";
                    return false;
                }
                else
                {
                    invoice = new udovika_invoice()
                    {
                        id = 0,
                        number = name,
                        date = DateTime.Now
                    };
                    SaveInvoice(invoice, user, out msg);
                    res = true;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, invoice, "");
            }
            return res;
        }
        public int SaveInvoice(udovika_invoice invoice, aspnet_Users user, out string msg)
        {
            msg = "";
            try
            {
                if (!GetPermissionAccessInvoice(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return 0;
                }
                else
                    db.SaveInvoice(invoice);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return invoice.id;
        }
        public bool DeleteInvoice(int id, aspnet_Users user, out string msg)
        {
            msg = "";
            bool res = false;
            //var item = new udovika_invoice();
            try
            {
                //item = db.GetInvoice(id);
                if (!GetPermissionAccessInvoice(user))
                {
                    msg = "Недостаточно прав.";
                    return res;
                }
                else
                {
                    db.DeleteInvoice(id);
                    msg = "Счет удален.";
                    res = true;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { invoiceId = id }, "");
                msg = "Счет не удален.";
            }
            return res;
        }
        #endregion
        #region InvoiceStatus
        public List<udovika_statusInvoice> GetStatuses()
        {
            var res = new List<udovika_statusInvoice>();
            try
            {
                res = db.GetInvoiceStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveStatusInvoice(udovika_statusInvoice status)
        {
            var res = 0;
            try
            {
                res = db.SaveStatusInvoice(status);
            }
            catch (Exception ex)
            {
                _debug(ex, new { status }, "statusField");
            }
            return res;
        }
        public udovika_statusInvoice GetInvoiceStatus(int id)
        {
            var res = new udovika_statusInvoice();
            try
            {
                res = db.GetInvoiceStatuses().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public bool EditInvoiceField(int id, string code, 
            string value, out string msg, aspnet_Users user)
        {
            msg = "";
            var res = false;
            var invoice = new udovika_invoice();
            try
            {
                invoice = GetInvoice(id, user, out msg);
                if (!GetPermissionAccessInvoice(user))
                {
                    msg = "Недостаточно прав для редактирования.";
                    return res;
                }
                if (invoice != null)
                {
                    switch (code)
                    {
                        case "status":
                            invoice.statusID = RDL.Convert.StrToInt(value, 0);
                            break;
                        case "comment":
                            invoice.note = value;
                            break;
                        case "number":
                            invoice.number = value;
                            break;
                        case "contractor":
                            invoice.contractorID = RDL.Convert.StrToInt(value, 0);
                            break;
                    }
                    res = true;
                    SaveInvoice(invoice, user, out msg);
                    msg = "Статус сохранен успешно.";
                }
                else
                    msg = "Ошибка.";

            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Ошибка. Статус не изменен.";
            }
            return res;
        }
        #endregion
    }
}
