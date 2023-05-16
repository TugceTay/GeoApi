

using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Repositories.EfCore;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace _5_LayeredArchitecture.Controllers

{
    [Route("api/Parcels")]
    [ApiController]
    public class ParcelsController : ControllerBase
    {
        /* //DI 
         * 
         //private readonly RepositoryContext _context; 
         public ParcelsController(RepositoryContext context)
         {
            _context = context;
        */


        /*
         // Repository Manager
         private readonly IRepositoryManager _manager; 
         public ParcelsController(IRepositoryManager manager)
         {
             _manager = manager;

        */



        // service manager
        private readonly IServiceManager _manager;

        public ParcelsController(IServiceManager manager)
        {
            _manager = manager;
        }







        [HttpGet]
        public IActionResult GetAllParcels()

        {
            //var parcels = _context.Parcels.ToList();

            /* // Repository Manager  
             var parcels = _manager.Parcel.GetAllParcels(false); */

            // service manager
            var parcels = _manager.ParcelService.GetAllParcels(false);
            var geoJSONFeatureCollection = ConvertToGeoJSON(parcels);

            // Return the FeatureCollection as a JSON object
            return Ok(geoJSONFeatureCollection);
        }






        [HttpGet("{id:int}")]
        public IActionResult GetOneParcel([FromRoute(Name = "id")] int id) //  [FromRoute(Name = "id")] ile id nin nereden geleceğini belirttik
        {
            /* LINQ

             var parcel = ApplicationContext
                .Parcels
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
             */
            try
            {
                /* var parcel = _context
                .Parcels
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();// bulamazsa null döndürür
                 if (parcel is null)
                     return NotFound(); //404

                 return Ok(parcel);*/


                /*  // Repository Manager
                  var parcel = _manager          
                 .Parcel
                 .GetOneParcelById(id, false); // false: değisiklikleri izleme*/


                // service manager 
                var parcel = _manager
                .ParcelService
                .GetOneParcelById(id, false);


                if (parcel is null)
                    return NotFound(); //404

                var geoJSONFeatureCollection = ConvertToGeoJSON(new List<Parcel> { parcel });

                return Ok(geoJSONFeatureCollection);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }






        [HttpPost] //kaynak olustur
        public IActionResult CreateOneParcel([FromBody] Parcel parcel)
        {
            try
            {
                if (parcel is null)
                    return BadRequest(); // 400 

                /*  _context.Parcels.Add(parcel);
                    _context.SaveChanges();// kalıcı olarak kaydeder*/


                /* // Repository Manager
                _manager.Parcel.CreateOneParcel(parcel);
                _manager.Save();*/


                // service manager
                _manager.ParcelService.CreateOneParcel(parcel);

                var geoJSONFeatureCollection = ConvertToGeoJSON(new List<Parcel> { parcel });

                return StatusCode(201, geoJSONFeatureCollection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        [HttpPut("{id:int}")]
        public IActionResult UpdateOneParcel([FromRoute(Name = "id")] int id,
            [FromBody] Parcel parcel)
        {

            try
            {
                if (parcel is null)
                    return BadRequest(); // 400 

                /*// check parcel? -
                var entity = _context
                    .Parcels
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();*/


                /* //Repository Manager
                 var entity = _manager
                 .Parcel
                 .GetOneParcelById(id, true); // update de değisiklikler izlenir
             if (entity is null)
                 return NotFound(); // 404
                                  
             if (id != parcel.Id)   // check id
                 return BadRequest(); // 400

             entity.ParcelNo = parcel.ParcelNo;
                entity.Pafta = parcel.Pafta;
                entity.Ada = parcel.Ada;
                entity.il = parcel.il;
                entity.ilce = parcel.ilce;
                entity.mahalle = parcel.mahalle;
                entity.nitelik = parcel.nitelik;


            // _context.SaveChanges();
               _manager.Save();
            return Ok(parcel);*/



                // service manager 
                _manager.ParcelService.UpdateOneParcel(id, parcel, true);
                return NoContent();//204



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }








        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneParcel([FromRoute(Name = "id")] int id)
        {

            try
            {
                /*var entity = _context 
               .Parcels
               .Where(b => b.Id.Equals(id))
               .SingleOrDefault();*/


                /* //Repository Manager  
                 var entity = _manager
                 .Parcel
                 .GetOneParcelById(id, false); // */


                /*_context.Parcels.Remove(entity);
                  _context.SaveChanges();*/

                /* //Repository Manager
                 _manager.Parcel.DeleteOneParcel(entity);
                 _manager.Save();*/

                // service manager
                _manager.ParcelService.DeleteOneParcel(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }






        [HttpPatch("{id:int}")] 

        /* PUT > application/json
          PATCH > application/json-patch+json*/

        public IActionResult PartiallyUpdateOneParcel([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Parcel> parcelPatch) //[FromBody] JsonPatchDocument<T>  verileri parametrelerle bağlamak için kullanılır
                                                            
                                                             
        {
            try
            {
                /*var entity = _context
                    .Parcels
                    .FirstOrDefault(b => b.Id.Equals(id));*/

                /*//Repository Manager
                var entity = _manager
                    .Parcel
                    .GetOneParcelById(id, true);*/

                // service manager
                var entity = _manager
                    .ParcelService
                    .GetOneParcelById(id, true);


                if (entity is null)
                    return NotFound(); // 404

                parcelPatch.ApplyTo(entity);

                //_context.SaveChanges();

                /*//Repository Manager
                _manager.Parcel.Update(entity);*/

                // service manager
                _manager.ParcelService.UpdateOneParcel(id, entity, true);

                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        //convert geojson 
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




        private static GeoJSON.Net.Feature.FeatureCollection ConvertToGeoJSON(IEnumerable<Parcel> parcels)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();

            foreach (var parcel in parcels)
            {
                var properties = new Dictionary<string, object>
        {
            { "Id", parcel.Id },
            { "ParselNo", parcel.ParselNo },
            { "Pafta", parcel.Pafta },
            { "Ada", parcel.Ada },
            { "il", parcel.il },
            { "ilce", parcel.ilce },
            { "mahalle", parcel.mahalle },
            { "nitelik", parcel.nitelik }
        };
                var geometry = ConvertGeometryToGeoJSON(parcel.geom);

                var feature = new Feature(geometry, properties);
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }

    }
}




