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
    
    public partial class as_dataTypes
    {
        public as_dataTypes()
        {
            this.as_settings = new HashSet<as_settings>();
            this.calc_parameters = new HashSet<calc_parameters>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<as_settings> as_settings { get; set; }
        public virtual ICollection<calc_parameters> calc_parameters { get; set; }
    }
}