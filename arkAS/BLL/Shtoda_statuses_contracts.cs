namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_statuses_contracts
    {
        public Shtoda_statuses_contracts()
        {
            this.Shtoda_contracts = new HashSet<Shtoda_contracts>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<Shtoda_contracts> Shtoda_contracts { get; set; }
    }
}
