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
    
    public partial class crm_clientStatuses
    {
        public crm_clientStatuses()
        {
            this.crm_clients = new HashSet<crm_clients>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
        public string state { get; set; }
    
        public virtual ICollection<crm_clients> crm_clients { get; set; }
    }
}
