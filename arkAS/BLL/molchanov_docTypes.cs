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
    
    public partial class molchanov_docTypes
    {
        public molchanov_docTypes()
        {
            this.molchanov_documents = new HashSet<molchanov_documents>();
            this.molchanov_logDocTypes = new HashSet<molchanov_logDocTypes>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<molchanov_documents> molchanov_documents { get; set; }
        public virtual ICollection<molchanov_logDocTypes> molchanov_logDocTypes { get; set; }
    }
}