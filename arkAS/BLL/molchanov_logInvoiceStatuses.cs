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
    
    public partial class molchanov_logInvoiceStatuses
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public string notice { get; set; }
        public string userName { get; set; }
        public int invoiceStatusID { get; set; }
    
        public virtual molchanov_invoiceStatuses molchanov_invoiceStatuses { get; set; }
    }
}
