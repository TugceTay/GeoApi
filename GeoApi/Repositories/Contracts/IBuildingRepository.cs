using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBuildingRepository
     : IRepositoryBase<Building>
    {
        IQueryable<Building> GetAllBuildings(bool trackChanges);
        Building GetOneBuildingById(int id, bool trackChanges);

        void CreateOneBuilding(Building building);
        void UpdateOneBuilding(Building building);
        void DeleteOneBuilding(Building building);

    }
}
