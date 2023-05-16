using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class ParcelRepository : RepositoryBase<Parcel>, IParcelRepository
    {
        public ParcelRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneParcel(Parcel parcel)
        => Create(parcel);

        public void DeleteOneParcel(Parcel parcel)
        => Delete(parcel);

        public IQueryable<Parcel> GetAllParcels(bool trackChanges)
        =>
            FindAll(trackChanges)
            .OrderBy(p => p.Id);
        public Parcel GetOneParcelById(int id, bool trackChanges)
        =>
            FindByCondition(p => p.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void UpdateOneParcel(Parcel parcel)
         => Update(parcel);
    }
}
