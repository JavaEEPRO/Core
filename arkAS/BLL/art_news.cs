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
    
    public partial class art_news
    {
        public int id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<int> typeID { get; set; }
        public string imgPath { get; set; }
        public string anouns { get; set; }
    
        public virtual art_newsType art_newsType { get; set; }
    }
}
