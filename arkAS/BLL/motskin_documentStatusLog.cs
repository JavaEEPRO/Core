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
    
    public partial class motskin_documentStatusLog
    {
        public int id { get; set; }
        public System.DateTime created { get; set; }
        public string note { get; set; }
        public int documentID { get; set; }
        public int documentStatusID { get; set; }
    
        public virtual motskin_documents motskin_documents { get; set; }
        public virtual motskin_documentStatuses motskin_documentStatuses { get; set; }
    }
}
