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
    
    public partial class fin_channels
    {
        public fin_channels()
        {
            this.fin_finances = new HashSet<fin_finances>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<fin_finances> fin_finances { get; set; }
    }
}
