//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arkAS.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class podvashetskyi_statuses_for_invoices
    {
        public podvashetskyi_statuses_for_invoices()
        {
            this.podvashetskyi_invoices = new HashSet<podvashetskyi_invoices>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<podvashetskyi_invoices> podvashetskyi_invoices { get; set; }
    }
}
