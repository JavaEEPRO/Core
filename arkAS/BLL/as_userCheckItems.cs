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
    
    public partial class as_userCheckItems
    {
        public as_userCheckItems()
        {
            this.as_userChecks = new HashSet<as_userChecks>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string emailText { get; set; }
        public string emailSubject { get; set; }
        public int setID { get; set; }
        public Nullable<System.DateTime> created { get; set; }
    
        public virtual as_userCheckSet as_userCheckSet { get; set; }
        public virtual ICollection<as_userChecks> as_userChecks { get; set; }
    }
}
