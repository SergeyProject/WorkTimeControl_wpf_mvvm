using System.Collections;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories.Abstract;

namespace WorkTimeControl.DAL.Repositories
{
    public class UserRepository : IUserRepository<UserEntity>
    {
        // Создать сотрудника
        public Guid Create(UserEntity user)
        {
            using (DataContext db = new DataContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.Id;
            }
        }

        // Удалить сотрудника
        public void Delete(Guid id)
        {
            using (DataContext db = new DataContext())
            {
                UserEntity user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        // Получить список всех сотрудников
        public IEnumerable GetAllUsers()
        {
            using (DataContext db = new DataContext())
            {
                return db.Users.ToList();
            }
        }

        // Получить сотрудника по ID 
        public UserEntity GetUserById(Guid id)
        {
            using (DataContext db = new DataContext())
            {
                return db.Users.Find(id);
            }
        }

        // Внести изменения в имени сотрудника
        public void UpdateUser(Guid id, UserEntity userEntity)
        {
            using (DataContext db = new DataContext())
            {
                UserEntity user = db.Users.Find(id);
                if (user != null)
                {
                    user.Name = userEntity.Name;
                    //db.Users.Update(user);
                    db.SaveChanges();
                }
            }
        }
    }
}
