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
    
    public partial class cl_lists
    {
        public cl_lists()
        {
            this.cl_listInstances = new HashSet<cl_listInstances>();
            this.cl_listItems = new HashSet<cl_listItems>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string roles { get; set; }
        public string users { get; set; }
    
        public virtual ICollection<cl_listInstances> cl_listInstances { get; set; }
        public virtual ICollection<cl_listItems> cl_listItems { get; set; }
    }
}