using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURNA.DAL.Access;
using ZURNA.DAL.Table;

namespace ZURNA.BL.BusinessLayer
{
    /// <summary>
    /// Strategy desing pattern
    /// burada gelen veriler üzerinde aksiyon gerçekleştirebiliriz.
    /// Buradan dönen sonuç direkt apiye gidecektir.
    /// </summary>
    public class BusinessManager<T>
    {
        public BusinessBase<T> business { get; set; }
        public BusinessManager(BusinessBase<T> businessbase)
        {
            business = businessbase;
        }
        
    }
    public abstract class BusinessBase<T>
    {
        public abstract Task<List<T>> GetList();
        public abstract Task<T> Create(T model,Guid guid);
        public abstract Task Delete(Guid guid);
        public abstract Task<T> Find(Guid guid);
        public abstract Task<T> Update(T model, Guid guid);
    }
    public class BussinessUser : BusinessBase<Users>
    {
        private FirebaseDataAccess<Users> _firedal;
        public BussinessUser()
        {


            _firedal = new FirebaseDataAccess<Users>();
            DataAccessManager<Users> dam = new DataAccessManager<Users>(_firedal);
        }

        public override async Task<Users> Create(Users model,Guid guid)
        {
            return await _firedal.CreateAsync(model,guid);
        }

        public override async Task Delete(Guid guid)
        {
            await _firedal.DeleteAsync(guid);
        }

        public override async Task<Users> Find(Guid guid)
        {
            return await _firedal.GetAsync(guid);
        }

        public override async Task<List<Users>> GetList()
        {
            var result= await _firedal.GetListAsync();
            return result;
        }

        public override async Task<Users> Update(Users model, Guid guid)
        {
            return await _firedal.UpdateAsync(guid, model);
        }

    }
    public class BusinessPost : BusinessBase<Post>
    {
        private FirebaseDataAccess<Post> _repo;
        public BusinessPost()
        {
            _repo = new FirebaseDataAccess<Post>();
            DataAccessManager<Post> dam = new DataAccessManager<Post>(_repo);
        }
        public override async Task<Post> Create(Post model, Guid guid)
        {
           return await _repo.CreateAsync(model, guid);
        }

        public override async Task Delete(Guid guid)
        {
            await _repo.DeleteAsync(guid);
        }

        public override async Task<Post> Find(Guid guid)
        {
            return await _repo.GetAsync(guid);
        }

        public override async Task<List<Post>> GetList()
        {
            return await _repo.GetListAsync();
        }

        public override async Task<Post> Update(Post model, Guid guid)
        {
            return await _repo.UpdateAsync(guid, model);
        }
        public async Task<List<Post>> GetPopularPost()
        {
            var result = await _repo.GetListAsync();
            result = result.OrderByDescending(sa => (sa.content.like + sa.content.dislike) / (sa.content.view + 1)).Take(100).ToList();
            return result;
        }
    }
}
