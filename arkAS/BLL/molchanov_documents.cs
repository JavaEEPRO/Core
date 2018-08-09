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
    
    public partial class molchanov_documents
    {
        public molchanov_documents()
        {
            this.molchanov_documents1 = new HashSet<molchanov_documents>();
            this.molchanov_logDocuments = new HashSet<molchanov_logDocuments>();
        }
    
        public int id { get; set; }
        public System.Guid uniqueCode { get; set; }
        public System.DateTime date { get; set; }
        public string number { get; set; }
        public decimal sum { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public int contragentID { get; set; }
        public int docTypeID { get; set; }
        public int docStatusID { get; set; }
        public Nullable<int> docParentID { get; set; }
    
        public virtual molchanov_contragents molchanov_contragents { get; set; }
        public virtual molchanov_docStatuses molchanov_docStatuses { get; set; }
        public virtual molchanov_docTypes molchanov_docTypes { get; set; }
        public virtual ICollection<molchanov_documents> molchanov_documents1 { get; set; }
        public virtual molchanov_documents molchanov_documents2 { get; set; }
        public virtual ICollection<molchanov_logDocuments> molchanov_logDocuments { get; set; }
    }
}
