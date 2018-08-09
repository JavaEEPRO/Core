using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Molchanov.Models
{
    public class MailViewModel
    {
        public List<molchanov_deliverySystems> DeliverySystems { get; set; }
        public List<molchanov_mailStatuses> MailStatuses { get; set; }
    }
}