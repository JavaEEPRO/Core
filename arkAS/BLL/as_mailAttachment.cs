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
    
    public partial class as_mailAttachment
    {
        public as_mailAttachment()
        {
            this.as_mail = new HashSet<as_mail>();
        }
    
        public int id { get; set; }
        public Nullable<int> mailID { get; set; }
        public Nullable<int> typeID { get; set; }
        public string link { get; set; }
        public string previewLink { get; set; }
    
        public virtual ICollection<as_mail> as_mail { get; set; }
        public virtual as_mail as_mail1 { get; set; }
        public virtual as_mailAttachmentType as_mailAttachmentType { get; set; }
    }
}
