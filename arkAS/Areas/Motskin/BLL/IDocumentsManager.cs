using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IDocumentsManager : 
        IBaseCRUDManager<motskin_documents>,
        IBaseStatusUseManager<motskin_documentStatuses, motskin_documentStatusLog>
    {
        List<motskin_documentTypes> GetTypes();
    }
}
