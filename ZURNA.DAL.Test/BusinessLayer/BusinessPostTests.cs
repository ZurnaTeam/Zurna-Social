using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZURNA.BL.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURNA.DAL.Access;
using ZURNA.DAL.Table;

namespace ZURNA.BL.BusinessLayer.Tests
{
    [TestClass()]
    public class BusinessPostTests
    {
        FirebaseDataAccess<Post> _repo = new FirebaseDataAccess<Post>();

        [TestMethod()]
        public async Task CreateTest()
        {
            Post post = new Post();
            Guid id = Guid.NewGuid();
            post.content = new Content()
            {
                dislike = 20,
                hashtag = "#GündemÖzel",
                like = 30,
                location = new Location()
                {
                    city = "Konya",
                    country = "Turkey",
                    lat = "34.12",
                    lon = "12.31"
                },
                text = "Bimde mükemmel bi indirim var akıllara ziyan !",
                time = DateTime.Now.AddDays(21)
            };
            post.comments = new List<Comment>();
            post.comments.Add(new Comment()
            {
                text = "Yav he he !",
                time = DateTime.Now.AddDays(20),
                userid = Guid.NewGuid()
            });
            post.userid = Guid.NewGuid();
            post.id = id;
            var result = await _repo.CreateAsync(post, id);
            if (result == null)
            {
                Assert.Fail("Ekleme işleminde hata oluştu");
            }

        }


        [TestMethod()]
        public async Task DeleteTest()
        {
            try
            {
                await _repo.DeleteAsync(Guid.Parse("29558a54-e4ad-4227-a602-edb903d24474"));
            }
            catch (Exception)
            {

                Assert.Fail("Silme testi hatalı");
            }
        }

        [TestMethod()]
        public async Task FindTest()
        {
            var finded = await _repo.GetAsync(Guid.Parse("43ffba59-620c-44c6-8576-07178d06b5cb"));
            if (finded == null) Assert.Fail();
            
        }

        [TestMethod()]
        public async Task GetListTest()
        {
            var result = await _repo.GetListAsync();
            if (result == null || result.Count <= 0) Assert.Fail();
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var finded = await _repo.GetAsync(Guid.Parse("43ffba59-620c-44c6-8576-07178d06b5cb"));
            finded.content.location.city = "ELAZIĞ";
            finded.content.location.country = "FINDLAND";
            finded.content.location.lat = "22";
            finded.content.location.lon = "11";
            var updated = await _repo.UpdateAsync(finded.id, finded);
            if (updated == null) Assert.Fail("Güncelleme testi hatalı");
        }
    }
}