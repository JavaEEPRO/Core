
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_contragents
    {
        public Shtoda_contragents()
        {
            this.Shtoda_contracts = new HashSet<Shtoda_contracts>();
            this.Shtoda_invoices = new HashSet<Shtoda_invoices>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Shtoda_contracts> Shtoda_contracts { get; set; }
        public virtual ICollection<Shtoda_invoices> Shtoda_invoices { get; set; }
        public virtual Shtoda_contragents Shtoda_contragents1 { get; set; }
        public virtual Shtoda_contragents Shtoda_contragents2 { get; set; }
    }
}
