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
    
    public partial class fedchenko_mailStatus
    {
        public fedchenko_mailStatus()
        {
            this.fedchenko_mail = new HashSet<fedchenko_mail>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<fedchenko_mail> fedchenko_mail { get; set; }
    }
}
