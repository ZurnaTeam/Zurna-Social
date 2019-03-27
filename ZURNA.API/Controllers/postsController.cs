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
    public class postsController : ApiController
    {
        private BusinessPost _post;
        public postsController()
        {
            _post = new BusinessPost();
            BusinessManager<Post> manager = new BusinessManager<Post>(_post);
            
        }
        [HttpGet,Route("api/posts")]
        public async Task<JsonResult<List<Post>>> Get()
        {
            var result= await _post.GetList();
            return Json(result);
        }            
        [Route("api/posts/{id}")]
        public async Task<JsonResult<Post>> Get(string id)
        {
            var result=  await _post.Find(Guid.Parse(id));
            return Json(result);
        }

        [HttpPut, Route("api/posts/{id}")]
        public async Task Put(string id, [FromBody]Post post)
        {
            await _post.Update(post, Guid.Parse(id));
        }
        [HttpPost,Route("api/posts/")]
        public async Task Post([FromBody]Post post)
        {
            await _post.Create(post, post.id);
        }
        [HttpDelete, Route("api/posts/{id}")]
        public async Task Delete(string id)
        {
            await _post.Delete(Guid.Parse(id));
        }

        [Route("api/post/popular")]
        public async Task<JsonResult<List<Post>>> GetPopularPost()
        {
            return Json(await _post.GetPopularPost());
        }

        [HttpPost,Route("api/posts/getcitypost")]
        public async Task<JsonResult<List<Post>>> GetCityPost([FromBody]City city)
        {
            return Json(await _post.GetCityPost(city.Name));
        }
    }
}
