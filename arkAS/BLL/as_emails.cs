//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class as_emails
    {
        public as_emails()
        {
            this.as_mailingLog = new HashSet<as_mailingLog>();
        }
    
        public int id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string attachment { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
    
        public virtual ICollection<as_mailingLog> as_mailingLog { get; set; }
    }
}
