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
    
    public partial class molchanov_mailStatuses
    {
        public molchanov_mailStatuses()
        {
            this.molchanov_mails = new HashSet<molchanov_mails>();
            this.molchanov_logMailStatuses = new HashSet<molchanov_logMailStatuses>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<molchanov_mails> molchanov_mails { get; set; }
        public virtual ICollection<molchanov_logMailStatuses> molchanov_logMailStatuses { get; set; }
    }
}
