using System.Collections;

namespace WorkTimeControl.DAL.Repositories.Abstract
{
    public interface IUserTimeRepository<T>where T : class
    {        
        Guid TimeStampCreate(T userTime);
        IEnumerable GetUserTimes(Guid userId);
        IEnumerable GetAllUserTime();
        void Delete(Guid userId);
    }
}
