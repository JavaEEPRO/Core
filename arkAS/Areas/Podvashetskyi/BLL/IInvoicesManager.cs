using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IInvoicesManager : IDisposable
    {
        #region Invoices
        List<pdv_invoices> GetInvoices();
        pdv_invoices GetInvoice(int id);
        void SaveInvoice(pdv_invoices item);
        bool AddInvoice(int number, pdv_contractors user);
        bool EditInvoiceField(int id, string code, string value, out string msg, pdv_contractors user);
        bool DeleteInvoice(int id, out string msg, pdv_contractors user);
        #endregion
        #region Invoices Statuses
        List<pdv_invoiceStatuses> GetInvoicesStatuses();
        pdv_invoiceStatuses GetInvoicesStatus(int id);
        int SaveInvoicesStatus(pdv_invoiceStatuses item, bool withSave = true);
        bool DeleteInvoiceStatus(int id);
        bool EditInvoiceStatusField(int id, string code, string value, out string msg);
        #endregion
    }
}
