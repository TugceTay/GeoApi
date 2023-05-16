using _3_locationDemo.Data;
using _3_locationDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace _3_locationDemo.Controllers
{
    [Route("api/[[locations]]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllLocations()
        {
            var locations = ApplicationContext.Locations;
            return Ok(locations);
        }





        [HttpGet("{id:int}")]
        public IActionResult GetOneLocation([FromRoute(Name = "id")] int id) //  [FromRoute(Name = "id")] ile id nin nereden geleceğini belirttik
        {
            /* LINQ
              
             var location = ApplicationContext
                .Locations
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
             */

            var location = ApplicationContext
                .Locations
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();// bulamazsa null döndürür


            if (location is null)
                return NotFound(); //404

            return Ok(location);
        }






        [HttpPost]
        public IActionResult CreateOneLocation([FromBody] Location location)
        {
            try
            {
                if (location is null)
                    return BadRequest(); // 400 
                ApplicationContext.Locations.Add(location);
                return StatusCode(201, location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // logger eklemeyi dene 

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneLocation([FromRoute(Name = "id")] int id,
            [FromBody] Location location) // location güncellenen veriyi tutmak için olusturulan nesne 
        {
            // check location? 
            var entity = ApplicationContext
                .Locations
                .Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound(); // 404
            // check id
            if (id != location.Id)
                return BadRequest(); // 400
            ApplicationContext.Locations.Remove(entity);// önce listeden siliyoruz
            location.Id = entity.Id;
            ApplicationContext.Locations.Add(location);
            return Ok(location);
        }





        [HttpDelete]
        public IActionResult DeleteAllLocations()
        {
            ApplicationContext.Locations.Clear();
            return NoContent(); // 204
        }





        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneLocation([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext // silinmek istenen kitap gerçekten var mı
                .Locations
                .Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Location with id:{id} could not found."
                });  // 404
            ApplicationContext.Locations.Remove(entity);
            return NoContent();
        }






        [HttpPatch("{id:int}")] // nesneyi kısmi olarak güncelleyebiliriz 
        /* 
           PUT > application/json
           PATCH > application/json-patch+json
         */

        public IActionResult PartiallyUpdateOneLocation([FromRoute(Name = "id")] int id,
             [FromBody] JsonPatchDocument<Location> locationPatch)
        //[FromBody] JsonPatchDocument<T>  verileri parametrelerle bağlamak için kullanılır
        {
            // check entity
            var entity = ApplicationContext.Locations.Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound(); // 404
            locationPatch.ApplyTo(entity);
            return NoContent(); // 204
        }
    }
}


