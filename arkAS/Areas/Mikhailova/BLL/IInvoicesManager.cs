using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Mikhailova.BLL
{
    public interface IInvoicesManager : IDisposable
    {
        #region Invoices

        List<Mikhailova_invoices> GetInvoices();
        Mikhailova_invoices GetInvoice(int id);
        void SaveInvoice(Mikhailova_invoices item);
        bool AddInvoice(int number, Mikhailova_contracts user);
        bool EditInvoiceField(int id, string code, string value, out string msg, Mikhailova_contracts user);
        bool DeleteInvoice(int id, out string msg, Mikhailova_contracts user);

        #endregion

        #region Invoices Statuses

        List<Mikhailova_statuses_invoces> GetInvoicesStatuses();
        Mikhailova_statuses_invoces GetInvoiceStatus(int id);

        #endregion

    }
}
