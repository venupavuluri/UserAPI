using Microsoft.AspNetCore.Mvc;
using UserAPI.Model;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/<UserController>/5
        [HttpGet("{emailAddress}")]
        public string Get(string emailAddress)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] PostUserRequestModel userRequest)
        {

        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Put([FromBody] PostUserRequestModel userRequest)
        {

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{emailAddress}")]
        public void Delete(string emailAddress)
        {

        }
    }
}
