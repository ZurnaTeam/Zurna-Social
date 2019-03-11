using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURNA.DAL.Database.Firebase;
using ZURNA.DAL.Table;

namespace ZURNA.DAL.Access
{
    /// <summary>
    /// Dependency injection pattern
    /// Bütün veritabanı erişim ve aksiyonları burada gerçekleştirilir.
    /// </summary>
    public class DataAccessManager<T>
    {
        public IDataAccess<T> _dal;
        public DataAccessManager(IDataAccess<T> dal)
        {
            _dal = dal;
        }

    }
    /// <summary>
    /// Temsilciler yardımı ile bütün tablolarda aynı CRUD işlemleri
    /// gerçekleştirebilir
    /// </summary>
    /// <typeparam name="T">Table Type[Users,Post]</typeparam>
    public interface IDataAccess<T>
    {
        //---Standart Crud işlemleri---
        Task<List<T>> GetListAsync(); 
        Task DeleteAsync(Guid guid);      
        Task<T> GetAsync(Guid guid);        
        Task<T> UpdateAsync(Guid guid,T model);        
        Task<T> CreateAsync(T model);
       
    }
    public class FirebaseDataAccess<T> : IDataAccess<T>
    {
        #region Firebase Repo
        private string _authentication = "Q20dKxiR9WNE0PCtSQMByaabvHecwizEyzjD7mUb";
        private string _baseurl = "https://zurna-dbc48.firebaseio.com/";
        private FireRepo<T> _repo;
        #endregion
        public FirebaseDataAccess()
        {
            _repo = new FireRepo<T>(_authentication, _baseurl, $"{typeof(T).Name}/");
        }
        public async Task<T> CreateAsync(T model)
        {
            await _repo.Add(model, Guid.NewGuid());
            return model;
        }

        public async Task DeleteAsync(Guid guid)
        {
            await _repo.Delete(guid);
        }

        public async Task<T> GetAsync(Guid guid)
        {
            return await _repo.Find(guid);
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _repo.GetList();
        }

        public async Task<T> UpdateAsync(Guid guid,T model)
        {
            return await _repo.Update(guid, model);
        }
    }

}
