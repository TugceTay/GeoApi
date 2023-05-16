using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {

        //repolara manager uzerinden erisim verecegiz
        IParcelRepository Parcel { get; }
        IBuildingRepository Building { get; }
        void Save();
    }
}
