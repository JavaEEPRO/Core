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
    
    public partial class gl_docs
    {
        public int id { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string number { get; set; }
        public Nullable<int> contragentID { get; set; }
        public Nullable<decimal> sum { get; set; }
        public string note { get; set; }
        public int docStatusID { get; set; }
        public string path { get; set; }
        public int docTypeID { get; set; }
        public Nullable<int> parentItemID { get; set; }
        public string name { get; set; }
        public Nullable<System.Guid> createdBy { get; set; }
    
        public virtual gl_contragents gl_contragents { get; set; }
        public virtual gl_docStatuses gl_docStatuses { get; set; }
        public virtual gl_docTypes gl_docTypes { get; set; }
        public virtual fin_contragents fin_contragents { get; set; }
    }
}
