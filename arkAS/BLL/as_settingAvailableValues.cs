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
    
    public partial class as_settingAvailableValues
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public Nullable<int> settingID { get; set; }
    
        public virtual as_settings as_settings { get; set; }
    }
}
