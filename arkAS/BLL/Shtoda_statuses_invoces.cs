
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_statuses_invoces
    {
        public Shtoda_statuses_invoces()
        {
            this.Shtoda_invoices = new HashSet<Shtoda_invoices>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<Shtoda_invoices> Shtoda_invoices { get; set; }
    }
}
