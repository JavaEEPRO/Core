using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IInvoicesManager
    {
        #region Invoices
        List<udovika_invoice> GetInvoices();
        udovika_invoice GetInvoice(int id, aspnet_Users user, out string msg);
        bool CreateInvoice(string name, aspnet_Users user, out string msg);
        int SaveInvoice(udovika_invoice invoice, aspnet_Users user, out string msg);
        bool DeleteInvoice(int id, aspnet_Users user, out string msg);
        #endregion
        #region Status
        List<udovika_statusInvoice> GetStatuses();
        int SaveStatusInvoice(udovika_statusInvoice status);
        udovika_statusInvoice GetInvoiceStatus(int id);
        bool EditInvoiceField(int id, string code, string value, out string msg, aspnet_Users user);
        #endregion
    }
}
