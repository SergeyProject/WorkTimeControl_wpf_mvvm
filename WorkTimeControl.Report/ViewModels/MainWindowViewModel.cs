using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services;
using WorkTimeControl.Report.Helper;
using WorkTimeControl.Report.Models;
using WorkTimeControl.WpfClient.Camera;

namespace WorkTimeControl.Report.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        Mediator md = new Mediator();
        public RelayCommand FindNameCommand { get; }
        public RelayCommand SelectionChangeCommand {  get; }
        public RelayCommand SelectionChangeUserTimeCommand { get; }
        public RelayCommand SelectionDateCommand { get; }
        public RelayCommand TextChangedCommand { get; }
        public RelayCommand SelectionUserTimeCommand { get; }

        public MainWindowViewModel()
        {
            FindNameCommand = new RelayCommand(LoadNamesList);
            SelectionChangeCommand = new RelayCommand(LoadUserTimesList);
            SelectionChangeUserTimeCommand=new RelayCommand(Msg);
            SelectionDateCommand=new RelayCommand(LoadUserTimesList);
            TextChangedCommand=new RelayCommand(TextChanged);
            SelectionUserTimeCommand = new RelayCommand(SetImageUserTime);
            SelectedDate = DateTime.Now;
            ListBoxIndex = -1;
            LoadAllUserList();
        }

      

        private void Msg()
        {
            MessageBox.Show("OK!");
        }

        private int _ListBoxIndex;
        public int ListBoxIndex
        {
            get { return _ListBoxIndex; }
            set
            {
                _ListBoxIndex = value;
                OnPropertyChanged(nameof(ListBoxIndex));
            }
        }

        private string _FindName;
        public string FindName
        {
            get { return _FindName; }
            set
            {
                _FindName = value;
                OnPropertyChanged(nameof(FindName));
            }
        }

        private ObservableCollection<UserDTO> _FindNamesList=new ObservableCollection<UserDTO>();
        public ObservableCollection<UserDTO> FindNamesList
        {
            get { return _FindNamesList; }
            set
            {
                _FindNamesList = value;
                OnPropertyChanged(nameof(FindNamesList));
            }
        }


        private void LoadNamesList()
        {
            FindNamesList.Clear();
            Services services = new Services();
            foreach(UserDTO user in services.GetAllUsers3())
            {
                if (user.Name.ToLower().Contains(FindName.ToLower()))
                {
                    FindNamesList.Add(user);                   
                }
            }
        }

        private void LoadAllUserList()
        {
            FindNamesList.Clear();
            Services services = new Services();
            foreach (UserDTO user in services.GetAllUsers3())
            {
                FindNamesList.Add(user);
            }
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private ObservableCollection<UserTimeDTO> _FindUserTimeList=new ObservableCollection<UserTimeDTO>();
        public ObservableCollection <UserTimeDTO> FindUserTimeList
        {
            get { return _FindUserTimeList;}
            set
            {
                _FindUserTimeList = value;
                OnPropertyChanged(nameof(FindUserTimeList));
            }
        }

        private ObservableCollection<ContentUserTime> _ContentUserTimeList;  //=new ObservableCollection<ContentUserTime>();
        public ObservableCollection<ContentUserTime> ContentUserTimeList
        {
            get { return _ContentUserTimeList; }
            set
            {
                _ContentUserTimeList = value;
                OnPropertyChanged(nameof(ContentUserTimeList));
            }
        }

        private void LoadUserTimesList()
        {
            ListViewIndex = -1;
            GetTimeImage = null;
            if (ListBoxIndex > -1)
            {
                ContentUserTimeList = new ObservableCollection<ContentUserTime>();
                md.UserId = FindNamesList[ListBoxIndex].Id;
                ServiceDTO service = new ServiceDTO();
                FindUserTimeList.Clear();
                ContentUserTimeList.Clear();
                foreach (UserTimeDTO item in service.UserTimeService().GetUserTimes(md.UserId))
                {
                    if (item.DateTimes.Date == SelectedDate.Date)
                    {
                        FindUserTimeList.Add(item);
                        ContentUserTime contentUserTime = new ContentUserTime()
                        {
                            WorkTime = item.DateTimes.ToShortTimeString(),
                            WorkDate= item.DateTimes.ToLongDateString(),
                            IsOnWork = item.IsOnWork ? "Пришел на работу" : "Ушел с работы",
                            Photo = item.Photo
                        };
                        ContentUserTimeList.Add(contentUserTime);
                    }
                }
            }

        }

        private void TextChanged()
        {
            LoadNamesList();
        }

        private BitmapImage _GetTimeImage;
        public BitmapImage GetTimeImage
        {
            get { return _GetTimeImage; }
            set
            {
                _GetTimeImage = value;
                OnPropertyChanged(nameof(GetTimeImage));
            }
        }

        private int _ListViewIndex;
        public int ListViewIndex
        {
            get { return _ListViewIndex; }
            set
            {
                _ListViewIndex = value;
                OnPropertyChanged(nameof(ListViewIndex));
            }
        }
        private void SetImageUserTime()
        {
            if (ListViewIndex > -1)
            {
                byte[] bytes = ContentUserTimeList[ListViewIndex].Photo;
                GetTimeImage = ImageConvert.ConvertByteToBitmapImage(bytes);
            }
        }
    }
}
