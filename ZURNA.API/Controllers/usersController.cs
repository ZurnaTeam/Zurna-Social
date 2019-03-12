using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ZURNA.BL.BusinessLayer;
using ZURNA.DAL.Table;

namespace ZURNA.API.Controllers
{
    public class usersController : ApiController
    {
        private BussinessUser _user;

        public usersController()
        {
            _user = new BussinessUser();
             BusinessManager<Users> manager = new BusinessManager<Users>(_user);
        }
        // GET api/users
        [HttpGet]
        public async Task<JsonResult<List<Users>>> Get()
        {
            var result = await _user.GetList();
            return Json(result);
        }
        // GET api/users/id
        [Route("api/users/{hash}")]
        public async Task<JsonResult<Users>> Get(string id)
        {
            var result = await _user.Find(Guid.Parse(id));
            return Json(result);
        }
        // POST api/users
        public async Task Post([FromBody]Users user)
        {
            await _user.Create(user,user.id);
        }
        // PUT api/users/id with Body(User)
        [HttpPut, Route("api/users/{id}")]
        public async Task Put(string id, [FromBody]Users user)
        {
            await _user.Update(user, Guid.Parse(id));
        }
        // DELETE api/users/id
        [HttpDelete, Route("api/users/{id}")]
        public async Task Delete(string id)
        {
            await _user.Delete(Guid.Parse(id));
        }

    }
}
