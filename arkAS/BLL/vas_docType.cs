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
    
    public partial class vas_docType
    {
        public vas_docType()
        {
            this.vas_documents = new HashSet<vas_document>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<vas_document> vas_documents { get; set; }
    }
}
