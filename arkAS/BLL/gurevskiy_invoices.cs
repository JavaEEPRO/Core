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
    
    public partial class gurevskiy_invoices
    {
        public int id { get; set; }
        public int statusID { get; set; }
        public int partnerID { get; set; }
        public System.DateTime date { get; set; }
        public string number { get; set; }
        public string comment { get; set; }
    
        public virtual gurevskiy_partner gurevskiy_partner { get; set; }
        public virtual gurevskiy_invoiceStatuses gurevskiy_invoiceStatuses { get; set; }
    }
}