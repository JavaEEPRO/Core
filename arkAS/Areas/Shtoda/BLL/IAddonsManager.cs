using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IAddonsManager : IDisposable
    {
        #region Addons

        List<Shtoda_addons> GetAddons();
        Shtoda_addons GetAddon(int id);
        void SaveAddon(Shtoda_addons item);
        bool AddAddon(int number, Shtoda_contracts user);
        bool EditAddonField(int id, string code, string value, out string msg, Shtoda_contracts user);
        bool DeleteAddon(int id, out string msg, Shtoda_contracts user);

        #endregion

        #region Addons Statuses

        List<Shtoda_statuses_addons> GetAddonsStatuses();
        Shtoda_statuses_addons GetAddonStatus(int id);

        #endregion

    }
}
