using System.Collections;
using WorkTimeControl.DAL.Entities;

namespace WorkTimeControl.DAL.Repositories.Abstract
{
    public interface IUserRepository<T>where T : class
    {
        Guid Create(T user);
        void Delete(Guid id);
        T GetUserById(Guid id);
        IEnumerable GetAllUsers();
        void UpdateUser(Guid id, UserEntity userEntity);
    }
}
