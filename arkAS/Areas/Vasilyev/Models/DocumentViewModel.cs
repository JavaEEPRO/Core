using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.Models
{
    public class DocumentViewModel
    {
        public List<vas_docStatuses> Statuses { get; set; }
        public List<vas_docTypes> Types { get; set; }
        public List<vas_contractors> Contractors { get; set; }
    }
}