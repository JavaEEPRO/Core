using arkAS.BLL;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.Models
{
    public class InvoiceViewModel
    {
        public List<tsilurik_status> Statuses { get; set; }
        public List<tsilurik_contractors> Contractors { get; set; }
    }
}