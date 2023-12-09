using System.Collections;
using WorkTimeControl.BLL.DTO;

namespace WorkTimeControl.BLL.Services.Interfaces
{
    public interface IUserService<T> where T : class
    {
        Guid Create(T user);
        void Delete(Guid id);
        T GetUserById(Guid id);
        IEnumerable GetAllUsers();
        void UpdateUser(Guid guid, UserDTO user);
    }
}
