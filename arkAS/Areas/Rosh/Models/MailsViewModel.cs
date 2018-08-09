using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.Models
{
    public class MailsViewModel
    {
        public List<rosh_mails> Mails { get; set; }
        public List<rosh_mailStatuses> mailStatuses { get; set; }
        public List<rosh_sendingSystems> SendingSystems { get; set; }
    }
}