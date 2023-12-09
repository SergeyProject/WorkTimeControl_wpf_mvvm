using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services;

namespace WorkTimeControl.WpfClient.ViewModels
{
    internal class AddWindowViewModel : ObservableObject
    {

        // Метод для создания нового сотрудника и закрытия окна в котором он создан
        private void AddNewUser()
        {
        //    UserDTO userDTO = new UserDTO() { Name = Name };
        //    ServiceDTO serviceDTO = new ServiceDTO();
        //    serviceDTO.UserService().Create(userDTO);
        //    WindowCloseV();
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
