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
    
    public partial class udovika_email
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string description { get; set; }
        public string system { get; set; }
        public int statusID { get; set; }
        public string trackNum { get; set; }
        public System.DateTime backDate { get; set; }
        public string backNum { get; set; }
    
        public virtual udovika_statusEmail udovika_statusEmail { get; set; }
    }
}