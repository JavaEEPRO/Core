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
    
    public partial class doc_docTypeTemplates
    {
        public int id { get; set; }
        public string name { get; set; }
        public int typeID { get; set; }
        public string path { get; set; }
        public Nullable<int> ord { get; set; }
    
        public virtual doc_docTypes doc_docTypes { get; set; }
    }
}
