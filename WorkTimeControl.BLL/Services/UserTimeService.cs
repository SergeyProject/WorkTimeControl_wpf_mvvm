using System.Collections;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services.Interfaces;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories.Abstract;

namespace WorkTimeControl.BLL.Services
{
    public class UserTimeService : IUserTimeService<UserTimeDTO>
    {
        private readonly IUserTimeRepository<UserTimeEntity> _userTimeRepository;
        public UserTimeService(IUserTimeRepository<UserTimeEntity> userTimeRepository)
        {
            _userTimeRepository = userTimeRepository;
        }

        public void Delete(Guid userId)
        {
            _userTimeRepository.Delete(userId);
        }

        public IEnumerable GetAllUserTime()
        {
           return _userTimeRepository.GetAllUserTime();
        }

        public IEnumerable GetUserTimes(Guid userId)
        {
            List<UserTimeDTO> userTimes = new List<UserTimeDTO>();
           foreach(UserTimeEntity userTimeEntity in _userTimeRepository.GetUserTimes(userId))
            {
                UserTimeDTO userTimeDTO = new UserTimeDTO()
                {
                    Id = userTimeEntity.Id,
                    DateTimes = userTimeEntity.DateTimes,
                    Descript = userTimeEntity.Descript,
                    IsOnWork = userTimeEntity.IsOnWork,
                    Photo = userTimeEntity.Photo,
                    UserId = userTimeEntity.UserId
                };
                userTimes.Add(userTimeDTO);
            }
           return userTimes;
        }

        public void TimeStampCreate(UserTimeDTO userTime)
        {
            UserTimeEntity userTimeEntity = new UserTimeEntity()
            {
                UserId = userTime.UserId,
                DateTimes = userTime.DateTimes,
                Descript = userTime.Descript,
                IsOnWork = userTime.IsOnWork,
                Photo = userTime.Photo
            };
           _userTimeRepository.TimeStampCreate(userTimeEntity);
        }
    }
}
