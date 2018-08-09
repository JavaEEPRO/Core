using arkAS.BLL;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.Models
{
    public class DocumentViewModel
    {
        public List<tsilurik_status> Statuses { get; set; }
        public List<tsilurik_types> Types { get; set; }
        public List<tsilurik_contractors> Contractors { get; set; }
    }
}