using _1_emptyTemplate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_emptyTemplate.Controllers
{
    [Route("api/[controller]")]//// ilgili endpointe ulasmak için
    [ApiController]// Controllerın Api yapısını desteklemesi için 
    public class LocationsController : ControllerBase
    {

        //[HttpGet]
        //public string getmessage()
        //{
        //    return "hi !";
        //}



        [HttpGet]
        public IActionResult GetMessage()
        {
            var result = new Location()
            {
                HttpStatus = 200,
                Message = "hello asp.net core web apı"
            };

            return Ok(result);
        }

    }
}
