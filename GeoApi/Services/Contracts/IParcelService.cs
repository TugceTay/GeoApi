using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IParcelService
    {
        IEnumerable<Parcel> GetAllParcels(bool trackChanges); // Parcel- ad referance to entities 
        Parcel GetOneParcelById(int id, bool trackChanges);
        Parcel CreateOneParcel(Parcel parcel);
        void UpdateOneParcel(int id, Parcel parcel, bool trackChanges);
        void DeleteOneParcel(int id, bool trackChanges);
    }
}





