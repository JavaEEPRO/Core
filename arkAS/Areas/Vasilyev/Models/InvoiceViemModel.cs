using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.Models
{
    public class InvoiceViemModel
    {
        public List<vas_invoiceStatuses> Statuses { get; set; }
        public List<vas_contractors> Contractors { get; set; }
    }
}