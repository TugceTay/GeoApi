using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBuildingService
    {
        IEnumerable<Building> GetAllBuildings(bool trackChanges); // Building- ad referance to entities 
        Building GetOneBuildingById(int id, bool trackChanges);
        Building CreateOneBuilding(Building building);
        void UpdateOneBuilding(int id, Building building, bool trackChanges);
        void DeleteOneBuilding(int id, bool trackChanges);
    }
}
