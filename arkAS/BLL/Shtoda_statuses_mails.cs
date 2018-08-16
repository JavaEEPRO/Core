
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_statuses_mails
    {
        public Shtoda_statuses_mails()
        {
            this.Shtoda_mails = new HashSet<Shtoda_mails>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<Shtoda_mails> Shtoda_mails { get; set; }
    }
}
