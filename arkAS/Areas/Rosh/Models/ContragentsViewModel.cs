using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.Models
{
    public class ContragentsViewModel
    {
        public List<rosh_contragents> Contragents { get; set; }
        public List<rosh_contragentSources> ContragentSources { get; set; }
        public List<rosh_contragentStatuses> ContragentStatuses { get; set; }
    }
}