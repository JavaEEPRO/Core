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
    
    public partial class gurevskiy_mails
    {
        public int id { get; set; }
        public int statusID { get; set; }
        public System.DateTime date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string desc { get; set; }
        public string systemSending { get; set; }
        public string numberTrack { get; set; }
        public string numberTrackBack { get; set; }
        public Nullable<System.DateTime> dateBack { get; set; }
    
        public virtual gurevskiy_mailStatuses gurevskiy_mailStatuses { get; set; }
    }
}
