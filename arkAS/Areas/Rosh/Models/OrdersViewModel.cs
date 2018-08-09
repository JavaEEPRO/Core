using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.Models
{
    public class OrdersViewModel
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public string orderNumber { get; set; }
        public int contragentID { get; set; }
        public string contragentName { get; set; }
        public int orderStatusID { get; set; }
        public string orderStatusName { get; set; }
        public string description { get; set; }

        public List<rosh_orders> Orders { get; set; }
        public List<rosh_contragents> Contragents { get; set; }
        public List<rosh_orderStatuses> OrdersStatuses { get; set; }
    }
}