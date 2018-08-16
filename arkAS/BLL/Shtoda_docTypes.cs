
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_docTypes
    {
        public Shtoda_docTypes()
        {
            this.Shtoda_contracts = new HashSet<Shtoda_contracts>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    
        public virtual ICollection<Shtoda_contracts> Shtoda_contracts { get; set; }
    }
}
