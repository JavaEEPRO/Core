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
    
    public partial class tst_drivers
    {
        public tst_drivers()
        {
            this.tst_cars = new HashSet<tst_cars>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<tst_cars> tst_cars { get; set; }
    }
}