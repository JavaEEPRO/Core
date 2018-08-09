using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.Models
{
    public class MailsView
    {
        public List<udovika_email> mails { get; set; }
        public List<udovika_statusEmail> mailStatuses { get; set; }
        public List<udovika_contractor> contractor { get; set; }
    }
}