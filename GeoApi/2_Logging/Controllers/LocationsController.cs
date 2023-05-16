using _2_Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2_Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly ILogger<LocationsController> _logger; // readonly olarak işaretlenen bir değişken, yalnızca ctor  tarafından veya tanımlandığı yerde başlatılabilir. Bir kez başlatıldıktan sonra, değeri değiştirilemez

        public LocationsController(ILogger<LocationsController> logger)
        {
            _logger = logger;
        }


        /*  DI
   
         * private readonly ILogger<LocationsController> _logger; // generate constructor

        public LocationsController(ILogger<LocationsController> logger)
        {
            _logger = logger;

        *ILogger<LocationsController> sınıfı, LocationsController sınıfında üretilen logların kaynağını belirtir. Bu sayede, uygulamanın herhangi bir yerinde bu log mesajlarına erişebilir ve hangi Controller veya sınıfın kaynaklık ettiğini kolayca tespit edebilirsiniz.
        }*/






        [HttpGet]
        public IActionResult GetAllLocations()
        {
            var locations = new List<Location>()
            {
             new Location() {Id=1, LocationName="L1", X= 35.1, Y=42.1},
                new Location() {Id=2, LocationName="L2", X= 35.2, Y=42.2},
                new Location() {Id=3, LocationName="L3", X= 35.3, Y=42.3}
            };
            _logger.LogInformation("GetAllLocations action has been called.");
            return Ok(locations);
        }



        [HttpPost]
        public IActionResult GetAllLocations([FromBody] Location location)
        {
            _logger.LogWarning("Location has been created");
            return StatusCode(201);
        }


    }
}
