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
    
    public partial class calc_calcs
    {
        public calc_calcs()
        {
            this.calc_parameters = new HashSet<calc_parameters>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string makeup { get; set; }
        public string resultFunction { get; set; }
        public string calcFunction { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<calc_parameters> calc_parameters { get; set; }
    }
}
