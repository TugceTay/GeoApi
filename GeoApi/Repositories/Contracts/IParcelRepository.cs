using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IParcelRepository : IRepositoryBase<Parcel>
    {
        IQueryable<Parcel> GetAllParcels(bool trackChanges);
        Parcel GetOneParcelById(int id, bool trackChanges);

        void CreateOneParcel(Parcel parcel);
        void UpdateOneParcel(Parcel parcel);
        void DeleteOneParcel(Parcel parcel);

    
    }
}
