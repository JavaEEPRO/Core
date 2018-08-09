using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IContragentsManager : IDisposable
    {
        #region Contragents
        List<molchanov_contragents> GetContragents(out string msg, aspnet_Users user);
        molchanov_contragents GetContragent(int id, out string msg, aspnet_Users user);
        molchanov_contragents CreateContragent(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_contragents EditContragent(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveContragent(int id, out string msg, aspnet_Users user);
        #endregion
    }
}
