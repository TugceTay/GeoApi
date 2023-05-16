using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BuildingManager : IBuildingService
{
    //DI
    private readonly IRepositoryManager _manager;

    public BuildingManager(IRepositoryManager manager)
    {
        _manager = manager;
    }


    public Building CreateOneBuilding(Building building)
    {
        _manager.Building.CreateOneBuilding(building);
        _manager.Save();
        return building;
    }


    public void DeleteOneBuilding(int id, bool trackChanges)
    {
        // check entity 
        var entity = _manager.Building.GetOneBuildingById(id, trackChanges);
        if (entity is null)
            throw new Exception($"Building with id :{id} could not found. ");

        _manager.Building.DeleteOneBuilding(entity);
        _manager.Save();
    }


    public IEnumerable<Building> GetAllBuildings(bool trackChanges)
    {
        return _manager.Building.GetAllBuildings(trackChanges);
    }


    public Building GetOneBuildingById(int id, bool trackChanges)
    {
        return _manager.Building.GetOneBuildingById(id, trackChanges);
    }


    public void UpdateOneBuilding(int id, Building building, bool trackChanges)
    {
        // check entity 
        var entity = _manager.Building.GetOneBuildingById(id, trackChanges);
        if (entity is null)
            throw new Exception($"Building with id :{id} could not found. ");
        // check params 
        if (building is null)
            throw new ArgumentNullException(nameof(building));

            entity.fKey = building.fKey;
            entity.Blok = building.Blok;
            entity.Nitelik = building.Nitelik;
            entity.geom = building.geom;

            _manager.Building.Update(entity);
        _manager.Save();
    }
  }
}
