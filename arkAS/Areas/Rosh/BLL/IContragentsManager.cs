using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public interface IContragentsManager : IDisposable
    {
        #region System
       // void Save();
        #endregion

        #region contragents
        List<rosh_contragents> GetContragents();
        rosh_contragents GetContragent(int id);
        void SaveContragent(rosh_contragents contragent);
        void DeleteContragent(int id);
        #endregion

        #region contragentSources
        List<rosh_contragentSources> GetContragentSources();
        rosh_contragentSources GetContragentSource(int id);
        void SaveContragentSource(rosh_contragentSources source);
        void DeleteContragentSource(int id);
        #endregion

        #region contragentStatuses
        List<rosh_contragentStatuses> GetContragentStatuses();
        rosh_contragentStatuses GetContragentStatus(int id);
        void SaveContragentStatus(rosh_contragentStatuses status);
        void DeleteContragentStatus(int id);
        #endregion
    }
}
