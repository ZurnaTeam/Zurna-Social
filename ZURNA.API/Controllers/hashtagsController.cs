using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using ZURNA.BL.BusinessLayer;
using ZURNA.BL.BusinessModel;
using ZURNA.DAL.Table;
using AcceptVerbsAttribute = System.Web.Http.AcceptVerbsAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ZURNA.API.Controllers
{
    public class hashtagsController : ApiController
    {
        private HashTagsBL _tagbl;
        private BusinessPost _post;
        public hashtagsController()
        {
            _post = new BusinessPost();
            _tagbl = new HashTagsBL();
        }
        [HttpPut,Route("api/hashtags/extractpost")]
        public JsonResult<HashtagModel> ExtractPost([FromBody]HashtagModel post)
        {
            return Json(_tagbl.GetExtractPost(post.OnlyText));
        }
        [Route("api/hashtags/getpopulartags")]
        public async Task<JsonResult<List<string>>> GetPopularTags()
        {
            var result = await _tagbl.GetPopularTags();
            return Json(result);
        }
        [HttpPost]
        [Route("api/hashtags/getcitypost")]
        public async Task<JsonResult<List<Post>>> GetCityPost([FromBody]City city)
        {
            var result = await _tagbl.GetCityPost(city.Name);
            return Json(result);
        }
        [HttpPost]
        [Route("api/hashtags/gettagpost")]
        public async Task<JsonResult<List<Post>>> GetTagPosts([FromBody]Tag tag)
        {
            
            //http://localhost:53943/api/hashtags/getcitypost/Konya
            var result = await _tagbl.GetPostByTagNames(tag.Name);
            return Json(result);
        }
    }
    public class City
    {
        public string Name { get; set; }
    }
    public class Tag
    {
        public string Name { get; set; }
    }

}
