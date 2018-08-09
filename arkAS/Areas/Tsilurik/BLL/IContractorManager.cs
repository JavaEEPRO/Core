using arkAS.BLL;
using System;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IContractorManager : IDisposable
    {
        List<tsilurik_contractors> GetContractors();
    }
}