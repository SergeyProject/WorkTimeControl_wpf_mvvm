using System.Collections;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories.Abstract;

namespace WorkTimeControl.DAL.Repositories
{
    public class UserTimeRepository : IUserTimeRepository<UserTimeEntity>
    {
        //Удаление всего списка времени связанного с конкретным сотрудником 
        public void Delete(Guid userId)
        {
            using (DataContext db = new DataContext())
            {
                IEnumerable userTimes = db.UserTimes.Where(e => e.UserId == userId).ToList();
                foreach (UserTimeEntity userTimesItem in userTimes)
                {
                    if (userTimesItem != null)
                    {
                        db.UserTimes.Remove(userTimesItem);
                        db.SaveChanges();
                    }
                }
            }
        }

        // Получение списка времени
        public IEnumerable GetAllUserTime()
        {
            using (DataContext db = new DataContext())
            {
                return db.UserTimes.ToList();
            }
        }

        // Получение списка времени конкретного сотрудника
        public IEnumerable GetUserTimes(Guid userId)
        {
            using (DataContext db = new DataContext())
            {
                return db.UserTimes.Where(e => e.UserId == userId).ToList();
            }
        }

        // Создать отметку времени
        public Guid TimeStampCreate(UserTimeEntity userTime)
        {
            using (DataContext db = new DataContext())
            {
                db.UserTimes.Add(userTime);
                db.SaveChanges();
                return userTime.Id;
            }
        }
    }
}
