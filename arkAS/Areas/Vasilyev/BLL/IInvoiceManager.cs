using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IInvoiceManager : IDisposable
    {
        #region Invoice
        vas_invoices GetInvoice(int id, aspnet_Users user, out string msg);
        List<vas_invoices> GetInvoices(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg);
        vas_invoices CreateInvoice(int contractorID, aspnet_Users user, out string msg);
        bool EditInvoiceField(int pk, string name, string value, aspnet_Users user, out string msg);
        bool DeleteInvoice(int id, aspnet_Users user, out string msg);
        #endregion

        #region Invoice Statuses
        vas_invoiceStatuses GetInvoiceStatus(int id);
        List<vas_invoiceStatuses> GetInvoiceStatuses();
        vas_invoiceStatuses CreateInvoiceStatus(string name, string code);
        void DeleteInvoiceStatus(int id);
        #endregion
    }
}