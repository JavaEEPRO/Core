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
    
    public partial class rosh_contragentSources
    {
        public rosh_contragentSources()
        {
            this.rosh_contragents = new HashSet<rosh_contragents>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<rosh_contragents> rosh_contragents { get; set; }
    }
}
