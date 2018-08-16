
namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shtoda_mails
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public string sender { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string sendSystem { get; set; }
        public string trackNumber { get; set; }
        public Nullable<int> status { get; set; }
        public string returnTrackNumber { get; set; }
        public System.DateTime returnDeliveryExpected { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual Shtoda_statuses_mails Shtoda_statuses_mails { get; set; }
    }
}
