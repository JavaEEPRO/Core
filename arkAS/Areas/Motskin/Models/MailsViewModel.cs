using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.Models
{
    public class MailsViewModel
    {
        public List<motskin_mailStatuses> Statuses { get; set; }
        public List<motskin_sendSystems> System { get; set; }
    }
}