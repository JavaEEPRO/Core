using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.Models
{
    public class DocumentView
    {
        public List<udovika_contractor> contractors { get; set; }
        public List<udovika_contractType> contractTypes { get; set; }
        public List<udovika_statusContract> contractStatuses { get; set; }
    }
}