using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZURNA.BL.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURNA.BL.BusinessLayer.Tests
{
    [TestClass()]
    public class BusinessPostTests
    {
        private BusinessPost _post;
        public BusinessPostTests()
        {
            _post = new BusinessPost();
        }
        [TestMethod()]
        public async Task GetCityPostTest()
        {
            try
            {
                var result = await _post.GetCityPost("Elazığ Merkez");
                
            }
            catch (Exception)
            {

                Assert.Fail("Şehir postlarını çağırmada hata oluştu");
            }

        }
    }
}