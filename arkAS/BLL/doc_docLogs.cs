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
    
    public partial class doc_docLogs
    {
        public int id { get; set; }
        public Nullable<int> docID { get; set; }
        public bool isDownload { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string createdBy { get; set; }
    
        public virtual doc_docs doc_docs { get; set; }
    }
}
