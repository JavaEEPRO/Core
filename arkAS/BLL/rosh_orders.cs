//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class rosh_orders
    {
        public rosh_orders()
        {
            this.rosh_documents = new HashSet<rosh_documents>();
            this.rosh_mails = new HashSet<rosh_mails>();
            this.rosh_orderLogs = new HashSet<rosh_orderLogs>();
        }
    
        public int id { get; set; }
        public System.DateTime orderDate { get; set; }
        public string orderNumber { get; set; }
        public int contragentID { get; set; }
        public int orderStatusID { get; set; }
        public string description { get; set; }
    
        public virtual rosh_contragents rosh_contragents { get; set; }
        public virtual ICollection<rosh_documents> rosh_documents { get; set; }
        public virtual ICollection<rosh_mails> rosh_mails { get; set; }
        public virtual ICollection<rosh_orderLogs> rosh_orderLogs { get; set; }
        public virtual rosh_orderStatuses rosh_orderStatuses { get; set; }
    }
}
