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
    
    public partial class ps_partners
    {
        public ps_partners()
        {
            this.ps_specsPartners = new HashSet<ps_specsPartners>();
        }
    
        public int id { get; set; }
        public string fio { get; set; }
        public string url { get; set; }
        public string desc { get; set; }
        public string experience { get; set; }
        public string technologies { get; set; }
        public string conditions { get; set; }
        public Nullable<int> specID { get; set; }
        public Nullable<int> statusID { get; set; }
    
        public virtual ICollection<ps_specsPartners> ps_specsPartners { get; set; }
        public virtual ps_statuses ps_statuses { get; set; }
    }
}
