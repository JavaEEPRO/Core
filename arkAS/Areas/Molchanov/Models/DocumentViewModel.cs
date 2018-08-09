using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.Models
{
    public class DocumentViewModel
    {
        public List<molchanov_contragents> Contragent { get; set; }
        public List<molchanov_docStatuses> DocStatuses { get; set; }
        public List <molchanov_docTypes> DocTypes { get; set; }
        public List <molchanov_documents> Documents { get; set; }
    }
}