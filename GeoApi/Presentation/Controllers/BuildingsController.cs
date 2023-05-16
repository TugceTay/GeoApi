
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Repositories.EfCore;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.Features;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System.Linq;

namespace _5_LayeredArchitecture.Controllers
{
    [Route("api/Buildings")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BuildingsController(IServiceManager manager)
        {
            _manager = manager;
        }





        [HttpGet]
        public IActionResult GetAllBuildings()

        {
           
            var buildings = _manager.BuildingService.GetAllBuildings(false);
            var geoJSONFeatureCollection = ConvertToGeoJSON(buildings);
            return Ok(geoJSONFeatureCollection);
        }










        [HttpGet("{id:int}")]
        public IActionResult GetOneBuilding([FromRoute(Name = "id")] int id) 
        {
           
            try
            {
               
                var building = _manager
                .BuildingService
                .GetOneBuildingById(id, false);


                if (building is null)
                    return NotFound(); //404

                var geoJSONFeatureCollection = ConvertToGeoJSON(new List<Building> { building });

                return Ok(geoJSONFeatureCollection);

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }






        [HttpPost] 
        public IActionResult CreateOneBuilding([FromBody] Building building)
        {
            try
            {
                if (building is null)
                    return BadRequest(); 
                _manager.BuildingService.CreateOneBuilding(building);

                var geoJSONFeatureCollection = ConvertToGeoJSON(new List<Building> { building });

                return StatusCode(201, geoJSONFeatureCollection);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }






        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBuilding([FromRoute(Name = "id")] int id,
            [FromBody] Building building)
        {

            try
            {
                if (building is null)
                    return BadRequest(); 
                
                _manager.BuildingService.UpdateOneBuilding(id, building, true);

                return NoContent();//204



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }









        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBuilding([FromRoute(Name = "id")] int id)
        {

            try
            {
              
                _manager.BuildingService.DeleteOneBuilding(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }






        [HttpPatch("{id:int}")] 
        public IActionResult PartiallyUpdateOneBuilding([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Building> buildingPatch)


        {
            try
            {
              
                var entity = _manager
                    .BuildingService
                    .GetOneBuildingById(id, true);


                if (entity is null)
                    return NotFound(); 

                buildingPatch.ApplyTo(entity); 
                _manager.BuildingService.UpdateOneBuilding(id, entity, true);
                return NoContent(); // 204

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }









        private static GeoJSON.Net.Geometry.MultiPolygon ConvertGeometryToGeoJSON(NetTopologySuite.Geometries.MultiPolygon ntsMultiPolygon)
        {
            var geoJSONMultiPolygon = new GeoJSON.Net.Geometry.MultiPolygon(
                ntsMultiPolygon.Geometries.Select(ntsPolygon =>
                    new Polygon(
                        new List<LineString>
                        {
                    new LineString(
                        ntsPolygon.Coordinates.Select(ntsCoordinate =>
                            new Position(ntsCoordinate.Y, ntsCoordinate.X)).ToList()
                    )
                        }
                    )).ToList()
            );

            return geoJSONMultiPolygon;
        }


        private static GeoJSON.Net.Feature.FeatureCollection ConvertToGeoJSON(IEnumerable<Building> buildings)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();

            foreach (var building in buildings)
            {
                var properties = new Dictionary<string, object>
        {
            { "Id", building.Id },
            { "fKey", building.fKey },
            { "Nitelik", building.Nitelik },
            { "Blok", building.Blok }
        };
 
                var geometry = ConvertGeometryToGeoJSON(building.geom);

                var feature = new Feature(geometry, properties);
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }
    }
}



