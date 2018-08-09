using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.Models
{
    public class DocumentViewModel
    {
        public List<motskin_documentStatuses> Statuses { get; set; }
        public List<motskin_documentTypes> Types { get; set; }
        public List<motskin_contractors> Contractors { get; set; }
    }
}