using Microsoft.AspNetCore.Mvc;

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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{emailAddress}")]
        public void Delete(string emailAddress)
        {
        }
    }
}
