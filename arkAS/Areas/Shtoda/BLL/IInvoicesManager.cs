using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IInvoicesManager : IDisposable
    {
        #region Invoices

        List<Shtoda_invoices> GetInvoices();
        Shtoda_invoices GetInvoice(int id);
        void SaveInvoice(Shtoda_invoices item);
        bool AddInvoice(int number, Shtoda_contracts user);
        bool EditInvoiceField(int id, string code, string value, out string msg, Shtoda_contracts user);
        bool DeleteInvoice(int id, out string msg, Shtoda_contracts user);

        #endregion

        #region Invoices Statuses

        List<Shtoda_statuses_invoces> GetInvoicesStatuses();
        Shtoda_statuses_invoces GetInvoiceStatus(int id);

        #endregion

    }
}
