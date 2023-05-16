using _4_EfCore.Models;
using _4_EfCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace _4_EfCore.Controllers
{
    [Route("api/[[Parcels]]")]
    [ApiController]
    public class ParcelsController : ControllerBase
    {
        //DI 
        private readonly RepositoryContext _context;

        public ParcelsController(RepositoryContext context)
        {
            _context = context;
        }





        [HttpGet]
        public IActionResult GetAllParcels()

        {
            var parcels = _context.Parcels.ToList();
            return Ok(parcels);

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
                var parcel = _context
               .Parcels
               .Where(b => b.Id.Equals(id))
               .SingleOrDefault();// bulamazsa null döndürür
                if (parcel is null)
                    return NotFound(); //404

                return Ok(parcel);
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
                _context.Parcels.Add(parcel);
                _context.SaveChanges();// kalıcı olarak kaydeder

                return StatusCode(201, parcel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







        [HttpPut("{id:int}")]
        public IActionResult UpdateOneParcel([FromRoute(Name = "id")] int id,
            [FromBody] Parcel parcel) // parcel güncellenen veriyi tutmak için olusturulan nesne 
        {
            try
            {
                // check parcel? // kitap var mı yok mu
                var entity = _context
                    .Parcels
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();
                if (entity is null)
                    return NotFound(); // 404
                                       // check id
                if (id != parcel.Id) // parametre olarak gönderilen id ile update edilmek istenen nesnenin id si uyusuyor mu
                    return BadRequest(); // 400

                entity.ParcelNo = parcel.ParcelNo;
                entity.Pafta = parcel.Pafta;
                entity.Ada = parcel.Ada;
                entity.il = parcel.il;
                entity.ilce = parcel.ilce;
                entity.mahalle = parcel.mahalle;
                entity.nitelik = parcel.nitelik;


                _context.SaveChanges();

                return Ok(parcel);
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
                var entity = _context // silinmek istenen kitap gerçekten var mı
               .Parcels
               .Where(b => b.Id.Equals(id))
               .SingleOrDefault();


                if (entity is null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Parcel with id:{id} could not found."
                    });  // 404
                _context.Parcels.Remove(entity);
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
        public IActionResult PartiallyUpdateOneParcel([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Parcel> parcelPatch)
        //[FromBody] JsonPatchDocument<T>  verileri parametrelerle bağlamak için kullanılır
        {
            try
            {
                var entity = _context
                    .Parcels
                    .FirstOrDefault(b => b.Id.Equals(id));

                if (entity is null)
                    return NotFound(); // 404

                parcelPatch.ApplyTo(entity);
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
