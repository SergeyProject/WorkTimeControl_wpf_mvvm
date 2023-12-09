using System.Collections;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services.Interfaces;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories.Abstract;

namespace WorkTimeControl.BLL.Services
{
    public class UserService : IUserService<UserDTO>
    {
        private readonly IUserRepository<UserEntity> _userRepository;

        public UserService(IUserRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public Guid Create(UserDTO user)
        {
            UserEntity entity = new UserEntity() { Name = user.Name };
            return _userRepository.Create(entity);
        }

        public void Delete(Guid id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable GetAllUsers()
        {
            List<UserDTO> userDTO = new List<UserDTO>();
           foreach(UserEntity userEntity in _userRepository.GetAllUsers())
            {
                UserDTO user = new UserDTO()
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name
                };
                userDTO.Add(user);
            }
           return userDTO;
        }

        public UserDTO GetUserById(Guid id)
        {
            UserEntity userEntity = _userRepository.GetUserById(id);
            UserDTO userDTO = new UserDTO
            {
                Id = userEntity.Id,
                Name = userEntity.Name
            };
            return userDTO;
        }

        public void UpdateUser(Guid guid, UserDTO user)
        {
            UserEntity userEntity = new UserEntity()
            {
                Id = guid,
                Name = user.Name,
            };
            _userRepository.UpdateUser(guid, userEntity);
        }
    }
}
