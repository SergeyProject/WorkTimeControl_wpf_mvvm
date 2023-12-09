using System.Collections;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories;

namespace WorkTimeControl.BLL.Services
{
    public class Services
    {

        public  List<UserDTO> GetUserList()
        {
            List<UserDTO> _list = new List<UserDTO>();
            _list.Clear();
            UserRepository userRepository = new UserRepository();
            foreach (UserDTO user in userRepository.GetAllUsers())
            {
                _list.Add(user);
            }
            return _list;
        }

        public void Create(string userName)
        {
            ServiceDTO service = new ServiceDTO();
            UserDTO userDTO = new UserDTO() { Name = userName };
            service.UserService().Create(userDTO);
        }

        public IEnumerable GetAllUsers()
        {
            ServiceDTO userDTO = new ServiceDTO();
            return userDTO.UserRepository().GetAllUsers();
        }

        public IEnumerable GetAllUsers2()
        {
            ServiceDTO userDTO = new ServiceDTO();
            return userDTO.UserService().GetAllUsers();
        }

        public List<UserDTO> GetAllUsers3()
        {
            List<UserDTO> _list = new List<UserDTO>();
            _list.Clear();
            UserRepository userRepository = new UserRepository();
           
            foreach(UserEntity userEntity in userRepository.GetAllUsers())
            {
                UserDTO userDTO = new UserDTO();
                userDTO.Id = userEntity.Id;
                userDTO.Name = userEntity.Name;
                _list.Add(userDTO);
            }
            return _list;
        }
    }
}
