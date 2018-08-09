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
    
    public partial class pdv_documents
    {
        public pdv_documents()
        {
            this.pdv_documents1 = new HashSet<pdv_documents>();
            this.pdv_invoices = new HashSet<pdv_invoices>();
        }
    
        public int id { get; set; }
        public string number { get; set; }
        public Nullable<int> contractorID { get; set; }
        public Nullable<decimal> sum { get; set; }
        public string comment { get; set; }
        public int documentStatusID { get; set; }
        public string link { get; set; }
        public System.DateTime createdDate { get; set; }
        public int documentTypeID { get; set; }
        public Nullable<int> documentID { get; set; }
        public string name { get; set; }
        public Nullable<System.Guid> createdBy { get; set; }
    
        public virtual pdv_contractors pdv_contractors { get; set; }
        public virtual ICollection<pdv_documents> pdv_documents1 { get; set; }
        public virtual pdv_documents pdv_documents2 { get; set; }
        public virtual pdv_documentType pdv_documentType { get; set; }
        public virtual pdv_documentStatuses pdv_documentStatuses { get; set; }
        public virtual ICollection<pdv_invoices> pdv_invoices { get; set; }
    }
}
