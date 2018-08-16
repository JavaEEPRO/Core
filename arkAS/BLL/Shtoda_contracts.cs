
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_contracts
    {
        public int id { get; set; }        
        public System.DateTime date { get; set; }
        public string number { get; set; }
        public int contragent { get; set; }
        public decimal total { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public string parent { get; set; }
        public Nullable<int> typeID { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual Shtoda_contragents Shtoda_contragents { get; set; }
        public virtual Shtoda_statuses_contracts Shtoda_statuses_contracts { get; set; }
        public virtual Shtoda_docTypes Shtoda_docTypes { get; set; }
    }
}
