using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IInvoiceManager : IDisposable
    {
        #region Invoice
        List<tsilurik_invoices> GetInvoices(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg,out int total);
        tsilurik_invoices GetInvoice(int id, aspnet_Users user, out string msg);
        tsilurik_invoices CreateInvoice(Dictionary<string,object> parameters, aspnet_Users user, out string msg);
        bool ChangeInvoiceStatus(int id, string statusID, string name, aspnet_Users user, out string msg);
        List<tsilurik_status> GetInvoiceStatuses();
        #endregion
    }
}