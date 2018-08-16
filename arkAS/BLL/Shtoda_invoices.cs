
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_invoices
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public string number { get; set; }
        public int contragentID { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual Shtoda_contragents Shtoda_contragents { get; set; }
        public virtual Shtoda_statuses_invoces Shtoda_statuses_invoces { get; set; }
    }
}
