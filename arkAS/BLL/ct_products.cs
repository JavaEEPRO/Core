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
    
    public partial class ct_products
    {
        public ct_products()
        {
            this.crm_orderItems = new HashSet<crm_orderItems>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> price { get; set; }
        public string desc { get; set; }
    
        public virtual ICollection<crm_orderItems> crm_orderItems { get; set; }
    }
}
