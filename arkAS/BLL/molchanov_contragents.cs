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
    
    public partial class molchanov_contragents
    {
        public molchanov_contragents()
        {
            this.molchanov_invoices = new HashSet<molchanov_invoices>();
            this.molchanov_documents = new HashSet<molchanov_documents>();
            this.molchanov_logContragents = new HashSet<molchanov_logContragents>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    
        public virtual ICollection<molchanov_invoices> molchanov_invoices { get; set; }
        public virtual ICollection<molchanov_documents> molchanov_documents { get; set; }
        public virtual ICollection<molchanov_logContragents> molchanov_logContragents { get; set; }
    }
}
