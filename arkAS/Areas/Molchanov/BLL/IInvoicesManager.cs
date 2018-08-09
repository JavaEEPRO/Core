using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IInvoicesManager : IDisposable
    {
        #region Invoices
        List<molchanov_invoices> GetInvoices(out string msg, aspnet_Users user);
        molchanov_invoices GetInvoice(int id, out string msg, aspnet_Users user);
        molchanov_invoices CreateInvoice(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_invoices EditInvoice(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool ChangeInvoicesStatus(int id, string value, string name, out string msg, aspnet_Users user);
        bool RemoveInvoice(int id, out string msg, aspnet_Users user);
        #endregion
        #region Invoice Status
        List <molchanov_invoiceStatuses> GetInvoiceStatuses(out string msg, aspnet_Users user);
        molchanov_invoiceStatuses GetInvoiceStatus(int id, out string msg, aspnet_Users user);
        molchanov_invoiceStatuses CreateInvoiceStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_invoiceStatuses EditInvoiceStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveInvoiceStatus(int id, out string msg, aspnet_Users user);
        #endregion
    }
}
