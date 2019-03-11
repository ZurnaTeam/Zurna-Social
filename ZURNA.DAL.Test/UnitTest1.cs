using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZURNA.DAL.Access;
using ZURNA.DAL.Table;

namespace ZURNA.DAL.Test
{
    [TestClass]
    public class UnitTest1
    {
        private FirebaseDataAccess<Users> _firedal;
        public UnitTest1()
        {
            _firedal = new FirebaseDataAccess<Users>();
            DataAccessManager<Users> _dam = new DataAccessManager<Users>(_firedal);
        }

        [TestMethod]
        public async Task AddTest()
        {
            Users users = new Users()
            {
                deviceid = "deneme_cihaz",
                isbanned = false,
                lastactivitydate = DateTime.Now,
                location = new Location() { city = "Elazig", country = "Turkey", lat = "23.021", lon = "31.12" },
                phone = "+905318121351",
                registerdate = DateTime.Now.AddDays(31)
            };
            Users result = await _firedal.CreateAsync(users,users.id);
            if (result == null)
            {
                throw new Exception("Ekleme işlemi hatalı");
            }
            
        }
        [TestMethod]
        public async Task FindTest()
        {
            Users result = await _firedal
                .GetAsync(Guid.Parse("1b49fe53-8ab3-41a7-8c37-553dd297243d"));
            if (result == null)
            {
                throw new Exception("Bulma işlemi hatalı");
            }
        }
        [TestMethod]
        public async Task ListTest()
        {
            var result = await _firedal.GetListAsync();
            Assert.AreEqual(result.Count, 1);
        }
        [TestMethod]
        public async Task DeleteTest()
        {
            await _firedal.DeleteAsync(Guid.Parse("2943bb94-7efd-4d34-ac11-1fe9bef82689"));
        }
    }
}
