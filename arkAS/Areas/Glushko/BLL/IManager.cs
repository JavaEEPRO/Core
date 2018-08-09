using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Glushko.BLL
{
    public interface IManager : IDisposable
    {
        IDocumentsManager Documents { get; set; }
    }
}
