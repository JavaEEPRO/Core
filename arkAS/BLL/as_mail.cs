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
    
    public partial class as_mail
    {
        public as_mail()
        {
            this.as_mailAttachment1 = new HashSet<as_mailAttachment>();
            this.as_mailLog = new HashSet<as_mailLog>();
        }
    
        public int id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public Nullable<int> attachmentID { get; set; }
        public Nullable<System.DateTime> create { get; set; }
        public string userName { get; set; }
    
        public virtual as_mailAttachment as_mailAttachment { get; set; }
        public virtual ICollection<as_mailAttachment> as_mailAttachment1 { get; set; }
        public virtual ICollection<as_mailLog> as_mailLog { get; set; }
    }
}
