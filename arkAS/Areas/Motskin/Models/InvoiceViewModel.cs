using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.Models
{
    public class InvoiceViewModel
    {
        public List<motskin_invoiceStatuses> Statuses { get; set; }
        public List<motskin_contractors> Contractors { get; set; }
    }
}