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
    
    public partial class vas_invoiceStatus
    {
        public vas_invoiceStatus()
        {
            this.vas_invoices = new HashSet<vas_invoice>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<vas_invoice> vas_invoices { get; set; }
    }
}
