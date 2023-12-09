using System.Collections;

namespace WorkTimeControl.BLL.Services.Interfaces
{
    public interface IUserTimeService<T> where T : class
    {
        void TimeStampCreate(T userTime);
        IEnumerable GetUserTimes(Guid userId);
        IEnumerable GetAllUserTime();
        void Delete(Guid userId);
    }
}
