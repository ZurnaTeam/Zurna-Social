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
}
