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
        [HttpGet]
        public async Task<JsonResult<List<Users>>> Get()
        {

            var result = await _user.GetList();
            return Json(result);
        }

        [Route("api/users/{hash}")]
        public async Task<JsonResult<Users>> Get(string hash)
        {
            var result = await _user.Find(Guid.Parse(hash));
            return Json(result);
        }

        public async Task Post([FromBody]Users user)
        {
            //Users user2 = new Users()
            //{
            //    id = new Guid(),
            //    deviceid = "asdasdad",
            //};
            await _user.Create(user,user.id);
        }

        //[HttpPut, Route("api/users/{id}")]
        //public async Task PutAsync(string id, [FromBody]User user)
        //{

        //}

    }
}
