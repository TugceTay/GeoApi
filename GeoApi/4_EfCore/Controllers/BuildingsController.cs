using _4_EfCore.Models;
using _4_EfCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace _4_EfCore.Controllers
{
    [Route("api/[[Buildings]]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        //DI 
        private readonly RepositoryContext _context;

        public BuildingsController(RepositoryContext context)
        {
            _context = context;
        }





        [HttpGet]
        public IActionResult GetAllBuildings()

        {
            var buildings = _context.Buildings.ToList();
            return Ok(buildings);

        }







        [HttpGet("{id:int}")]
        public IActionResult GetOneBuilding([FromRoute(Name = "id")] int id) //  [FromRoute(Name = "id")] ile id nin nereden geleceğini belirttik
        {
        
            try
            {
                var building = _context
               .Buildings
               .Where(b => b.Id.Equals(id))
               .SingleOrDefault();// bulamazsa null döndürür
                if (building is null)
                    return NotFound(); //404

                return Ok(building);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }







        [HttpPost] //kaynak olustur
        public IActionResult CreateOneBuilding([FromBody] Building building)
        {
            try
            {
                if (building is null)
                    return BadRequest(); // 400 
                _context.Buildings.Add(building);
                _context.SaveChanges();// kalıcı olarak kaydeder

                return StatusCode(201, building);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBuildings([FromRoute(Name = "id")] int id,
            [FromBody] Building building) 
        {
            try
            {
                
                var entity = _context
                    .Buildings
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();
                if (entity is null)
                    return NotFound(); // 404
                                       // check id
                if (id != building.Id) // parametre olarak gönderilen id ile update edilmek istenen nesnenin id si uyusuyor mu
                    return BadRequest(); // 400

                entity.fKey = building.fKey;
                entity.Blok = building.Blok;
                entity.Nitelik = building.Nitelik;

                _context.SaveChanges();

                return Ok(building);
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
                var entity = _context // silinmek istenen kitap gerçekten var mı
               .Buildings
               .Where(b => b.Id.Equals(id))
               .SingleOrDefault();


                if (entity is null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Building with id:{id} could not found."
                    });  // 404
                _context.Buildings.Remove(entity);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }






        [HttpPatch("{id:int}")] // nesneyi kısmi olarak güncelleyebiliriz 
        /*
         * 
           PUT > application/json
           PATCH > application/json-patch+json
         */
        public IActionResult PartiallyUpdateOneBuilding([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Building> building)
        //[FromBody] JsonPatchDocument<T>  verileri parametrelerle bağlamak için kullanılır
        {
            try
            {
                var entity = _context
                    .Buildings
                    .FirstOrDefault(b => b.Id.Equals(id));

                if (entity is null)
                    return NotFound(); // 404

                building.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}

