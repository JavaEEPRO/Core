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
    
    public partial class ps_specsPartners
    {
        public int id { get; set; }
        public Nullable<int> specID { get; set; }
        public Nullable<int> partnerID { get; set; }
    
        public virtual ps_partners ps_partners { get; set; }
        public virtual ps_specs ps_specs { get; set; }
    }
}
