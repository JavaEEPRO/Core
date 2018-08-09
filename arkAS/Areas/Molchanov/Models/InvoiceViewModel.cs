using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Molchanov.Models
{
    public class InvoiceViewModel
    {
        public List<molchanov_contragents> Contragents { get; set; } 
        public List<molchanov_invoiceStatuses> invStatuses { get; set; }
    }
}