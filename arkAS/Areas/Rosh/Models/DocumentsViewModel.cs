using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.Models
{
    public class DocumentsViewModel
    {
        public List<rosh_documents> Documents { get; set; }
        public List<rosh_docTypes> DocTypes { get; set; }
        public List<rosh_docStatuses> DocStatuses { get; set; }
        public List<rosh_contragents> Contragents { get; set; }
    }
}