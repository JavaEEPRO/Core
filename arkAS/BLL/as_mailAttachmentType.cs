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
    
    public partial class as_mailAttachmentType
    {
        public as_mailAttachmentType()
        {
            this.as_mailAttachment = new HashSet<as_mailAttachment>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<as_mailAttachment> as_mailAttachment { get; set; }
    }
}