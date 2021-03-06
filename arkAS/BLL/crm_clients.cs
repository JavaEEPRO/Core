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
    
    public partial class crm_clients
    {
        public crm_clients()
        {
            this.crm_orders = new HashSet<crm_orders>();
            this.fin_contragents = new HashSet<fin_contragents>();
        }
    
        public int id { get; set; }
        public string fio { get; set; }
        public string note { get; set; }
        public string city { get; set; }
        public Nullable<int> sourceID { get; set; }
        public Nullable<int> statusID { get; set; }
        public string addedBy { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string subchannel { get; set; }
        public string username { get; set; }
        public Nullable<bool> needActive { get; set; }
        public string leadSeller { get; set; }
        public Nullable<System.DateTime> nextContact { get; set; }
    
        public virtual crm_clientStatuses crm_clientStatuses { get; set; }
        public virtual crm_sources crm_sources { get; set; }
        public virtual ICollection<crm_orders> crm_orders { get; set; }
        public virtual ICollection<fin_contragents> fin_contragents { get; set; }
    }
}
