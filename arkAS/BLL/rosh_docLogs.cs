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
    
    public partial class rosh_docLogs
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<int> documentID { get; set; }
        public string changes { get; set; }
    
        public virtual rosh_documents rosh_documents { get; set; }
    }
}
