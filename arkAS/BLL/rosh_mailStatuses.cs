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
    
    public partial class rosh_mailStatuses
    {
        public rosh_mailStatuses()
        {
            this.rosh_mails = new HashSet<rosh_mails>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<rosh_mails> rosh_mails { get; set; }
    }
}
