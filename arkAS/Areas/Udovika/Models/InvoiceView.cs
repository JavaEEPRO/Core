using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.Models
{
    public class InvoiceView
    {
        public List<udovika_invoice> invoices { get; set; }
        public List<udovika_statusInvoice> invoiceStatuses { get; set; }
        public List<udovika_contractor> contractor { get; set; }
    }
}