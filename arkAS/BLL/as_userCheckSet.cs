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
    
    public partial class as_userCheckSet
    {
        public as_userCheckSet()
        {
            this.as_userCheckItems = new HashSet<as_userCheckItems>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string roles { get; set; }
    
        public virtual ICollection<as_userCheckItems> as_userCheckItems { get; set; }
    }
}
