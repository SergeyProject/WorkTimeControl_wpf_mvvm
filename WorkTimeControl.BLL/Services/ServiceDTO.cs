using Autofac;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services.Interfaces;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories;
using WorkTimeControl.DAL.Repositories.Abstract;

namespace WorkTimeControl.BLL.Services
{
    public class ServiceDTO
    {

        public IUserService<UserDTO> UserService()
        {
            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserService>().As<IUserService<UserDTO>>();
            userBuilder.RegisterType<UserRepository>().As<IUserRepository<UserEntity>>();
            return userBuilder.Build().Resolve<IUserService<UserDTO>>();
        }

        public IUserTimeService<UserTimeDTO> UserTimeService()
        {
            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserTimeService>().As<IUserTimeService<UserTimeDTO>>();
            userBuilder.RegisterType<UserTimeRepository>().As<IUserTimeRepository<UserTimeEntity>>();
            return userBuilder.Build().Resolve<IUserTimeService<UserTimeDTO>>();
        }

        public IUserRepository<UserEntity> UserRepository()
        {
            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserRepository>().As<IUserRepository<UserEntity>>();
            userBuilder.RegisterType<UserService>().As<IUserService<UserDTO>>(); 
            return userBuilder.Build().Resolve<IUserRepository<UserEntity>>();
        }
    }
}
