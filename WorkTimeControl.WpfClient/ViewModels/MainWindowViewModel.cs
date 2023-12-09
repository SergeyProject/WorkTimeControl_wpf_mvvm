using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using System;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services;
using WorkTimeControl.WpfClient.Camera;
using WorkTimeControl.WpfClient.Camera.Abstracts;
using WorkTimeControl.WpfClient.DataModels;
using WorkTimeControl.WpfClient.Views;
using System.Windows.Threading;
//using System.Windows.Controls;

namespace WorkTimeControl.WpfClient.ViewModels
{
    internal class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        private RelayCommand<string> buttonCommand;
        public RelayCommand ApplicationExitCommand { get; }
        public RelayCommand AddWindowOpenCommand { get; }
        public RelayCommand WindowCloseCommand { get; }
        public RelayCommand AddNewUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand UpdateUserCommand { get; }
        public RelayCommand WindowUserUpdateCommand { get; }
        public RelayCommand SelectionChangeCommand { get; }
        public RelayCommand AddUserTimeWorkOnCommand { get; }
        public RelayCommand AddUserTimeWorkOffCommand { get; }
        public RelayCommand SelectionUserTimeCommand { get; }
        public RelayCommand CameraInitialCommand { get; }
        //public VCapture vCapture;
        private DispatcherTimer Timer;
        public MainWindowViewModel()
        {
            ApplicationExitCommand = new RelayCommand(ApplicationExit);
            AddWindowOpenCommand = new RelayCommand(AddWindowOpn);
            WindowCloseCommand = new RelayCommand(WindowCloseV);
            AddNewUserCommand = new RelayCommand(AddNewUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            UpdateUserCommand = new RelayCommand(UpdateUser);
            WindowUserUpdateCommand = new RelayCommand(UpdateUsereWindowOpen);
            SelectionChangeCommand = new RelayCommand(GetUserTime);
            AddUserTimeWorkOffCommand = new RelayCommand(AddUserTimeOff);
            AddUserTimeWorkOnCommand = new RelayCommand(AddUserTimeOn);
            SelectionUserTimeCommand = new RelayCommand(SetImageUserTime);
            CameraInitialCommand = new RelayCommand(Initial);

            ButtonOnWorkEnabled = false;
            ButtonOnWorkDisabled = false;

            Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
            Timer.Tick += Timer_Tick;
            Timer.Start();
            ListUserIndex = -1;
            Initial();
            userDTOs();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            CurrentTime = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}";
        }

       

        private string _CurrentTime;
        public string CurrentTime
        {
            get { return _CurrentTime; }
            set
            {
                _CurrentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        private UserDTO _userDTO;
        public UserDTO userDTO
        {
            get { return _userDTO; }
            set
            {
                _userDTO = value;
                OnPropertyChanged(nameof(userDTO));
            }
        }
        
        private void ApplicationExit()
        {
            Application.Current.Shutdown(); 
        }

        // Список сотрудников
        private ObservableCollection<UserDTO> _UsersD = new ObservableCollection<UserDTO>();

        public ObservableCollection<UserDTO> UsersD
        {
            get
            {
                return _UsersD;
            }
            set
            {
                _UsersD = value;
                OnPropertyChanged("UserD");
            }
        }


        // Загрузка списка сотрдников
        private void userDTOs()
        {
            UsersD.Clear();
            Services services = new Services();
            foreach (UserDTO user in services.GetAllUsers3())
            {
                UsersD.Add(user);
                //UserDList.Add(user);
            }
            //UserDList.Sort((x, y) => string.Compare(x.Name, y.Name));
            ListCount = UsersD.Count;
        }

        
        ///////////////////////////////////////   List<T>   ///////////////////////////////////////////////////
       //private List<UserDTO> _UserDList=new List<UserDTO>();
       // public List<UserDTO> UserDList
       // {
       //     get { return _UserDList; }
       //     set
       //     {
       //         _UserDList = value;
       //         OnPropertyChanged(nameof(UserDList));
       //     }
       // }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // Статическая переменная хранения созданного окна
        private static Window? _window = null;

        // Создание окна для добавления нового сотрудника
        private void AddWindowOpn()
        {
            AddWindow addWindow = new AddWindow();
            _window = addWindow;
            addWindow.ShowDialog();
            userDTOs();
        }

        // Закрытие созданного окна
        private void WindowCloseV()
        {
            //Mediator.ListUserIndex = ListUserIndex;
            _window.Close();
            ListUserIndex = Mediator.ListUserIndex;
        }

        // Метод создания нового окна для внесения изменений в имени сотрудника...
        private void UpdateUsereWindowOpen()
        {
            if (MessageBox.Show("Вы хотите внести изменения в имени текущего сотрудника?", "Изменение имени сотрудника", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                GetName = GetUserNameById(UsersD[ListUserIndex].Id);
                UpdateUserWindow updateUserWindow = new UpdateUserWindow();
                _window = updateUserWindow;
                Mediator.ListUserIndex = ListUserIndex;
                updateUserWindow.ShowDialog();
                userDTOs();
            }
        }

        private static string _GetName;
        public string GetName
        {
            get { return _GetName; }
            set
            {
                _GetName = value;
                OnPropertyChanged();
            }
        }

        private string GetUserNameById(Guid id)
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            return serviceDTO.UserService().GetUserById(id).Name;
        }

        // Свойство хранения имени сотрудника
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        // Метод для создания нового сотрудника и закрытия окна в котором он создан
        private void AddNewUser()
        {
            UserDTO userDTO = new UserDTO() { Name = Name };
            ServiceDTO serviceDTO = new ServiceDTO();
            serviceDTO.UserService().Create(userDTO);
            WindowCloseV();
        }


        // Свойство хранения индекса в списке текущего сотрудника       
        private static int _ListUserIndex;
        public int ListUserIndex
        {
            get
            {
                return _ListUserIndex;
            }
            set
            {
                _ListUserIndex = value;             
                OnPropertyChanged(nameof(ListUserIndex));
            }
        }


        // Метод удаления текущего сотрудника
        private void DeleteUser()
        {
            if (MessageBox.Show("Текущий сотрудник будет удален\r\nПродолжить?", "Удаление сотрудника", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                ServiceDTO serviceDTO = new ServiceDTO();
                serviceDTO.UserService().Delete(UsersD[ListUserIndex].Id);
                userDTOs();
            }
        }

        // Метод изменения имени текущего сотрудника-------
        private void UpdateUser()
        {
            ListUserIndex = Mediator.ListBoxIndex;

            Guid id = UsersD[ListUserIndex].Id;
            UserDTO userDTO = new UserDTO() { Id = id, Name = GetName };
            ServiceDTO serviceDTO = new ServiceDTO();
            serviceDTO.UserService().UpdateUser(id, userDTO);
            WindowCloseV();
            ////idx = ListUserIndex;
        }

        // Свойство хранения длины списка
        private int _ListCount;
        public int ListCount
        {
            get { return _ListCount; }
            set
            {
                _ListCount = value;
                OnPropertyChanged(nameof(ListCount));
            }
        }

        // Список времени сотрудника
        private ObservableCollection<UserTimeDTO> _UserTimeList = new ObservableCollection<UserTimeDTO>();
        public ObservableCollection<UserTimeDTO> UserTimeList
        {
            get { return _UserTimeList; }
            set
            {
                _UserTimeList = value;
                OnPropertyChanged(nameof(UserTimeList));
            }
        }

        // Метод получения списка времени сотрудника
        private void GetUserTime()
        {
            ListUserTimeIndex = -1;
            Mediator.ListBoxIndex = ListUserIndex;
            GetTimeImage = null; 
            if (ListUserIndex > -1)
            {
                GetUserId = UsersD[ListUserIndex].Id;
                UserTimeList.Clear();
                ContentUserTimesList.Clear();
                ServiceDTO serviceDTO = new ServiceDTO();
                foreach (UserTimeDTO item in serviceDTO.UserTimeService().GetUserTimes(GetUserId))
                {
                    if (item.DateTimes.Date == DateTime.Now.Date)
                    {
                        UserTimeList.Add(item);
                        ContentUserTime contentUserTime = new ContentUserTime();
                        if (item.IsOnWork)
                        {
                            contentUserTime.OnWork = "Пришёл на работу:";
                            contentUserTime.Time = item.DateTimes.ToShortTimeString();
                            contentUserTime.Photo = item.Photo;
                        }
                        if (!item.IsOnWork)
                        {
                            contentUserTime.OnWork = "Ушёл с работы:";
                            contentUserTime.Time = item.DateTimes.ToShortTimeString();
                            contentUserTime.Photo = item.Photo;
                        }
                        ContentUserTimesList.Add(contentUserTime);
                    }
                }
                ButtonsControl();
            }
            else
            {
                //ContentUserTimesList.Clear();
                ListUserIndex = Mediator.ListBoxIndex;
            }
        }
        

        private ObservableCollection<ContentUserTime> _ContentUserTimesList = new ObservableCollection<ContentUserTime>();
        public ObservableCollection<ContentUserTime> ContentUserTimesList
        {
            get { return _ContentUserTimesList; }
            set
            {
                _ContentUserTimesList = value;
                OnPropertyChanged(nameof(ContentUserTimesList));
            }
        }

        // Добавить время прихода
        private void AddUserTimeOn()
        {           
            ServiceDTO serviceDTO = new ServiceDTO();
            UserTimeDTO userTimeDTO = new UserTimeDTO()
            {
                UserId = UsersD[ListUserIndex].Id,
                DateTimes = DateTime.Now,
                Descript = "Пришел на работу",
                IsOnWork = true,
                Photo = ImageConvert.ConvertToByte((Bitmap)ImagePath)
            };
            serviceDTO.UserTimeService().TimeStampCreate(userTimeDTO);
            GetUserTime();
        }

        // Добавить время ухода
        private void AddUserTimeOff()
        {           
            ServiceDTO serviceDTO = new ServiceDTO();
            UserTimeDTO userTimeDTO = new UserTimeDTO()
            {
                UserId = UsersD[ListUserIndex].Id,
                DateTimes = DateTime.Now,
                Descript = "Ушел с работы",
                IsOnWork = false,
                Photo = ImageConvert.ConvertToByte((Bitmap)ImagePath)
            };
            serviceDTO.UserTimeService().TimeStampCreate(userTimeDTO);
            GetUserTime();
        }


        // Хранение ID сотрудника
        private static Guid _GetUserId;
        public Guid GetUserId
        {
            get { return _GetUserId; }
            set
            {
                _GetUserId = value;
                OnPropertyChanged(nameof(GetUserId));
            }
        }

        private bool _ButtonOnWorkEnabled;
        public bool ButtonOnWorkEnabled
        {
            get { return _ButtonOnWorkEnabled; }
            set
            {
                _ButtonOnWorkEnabled = value;
                OnPropertyChanged(nameof(ButtonOnWorkEnabled));
            }
        }

        private bool _ButtonOnWorkDisabled;
        public bool ButtonOnWorkDisabled
        {
            get { return _ButtonOnWorkDisabled; }
            set
            { 
            _ButtonOnWorkDisabled = value;
            OnPropertyChanged(nameof(ButtonOnWorkDisabled));
            }
        }

        private void ButtonsControl()
        {
            if (UserTimeList.Count > 0)
            {
                ButtonOnWorkDisabled = UserTimeList[UserTimeList.Count - 1].IsOnWork;
                ButtonOnWorkEnabled = !UserTimeList[UserTimeList.Count - 1].IsOnWork;
            }
            else
            {
                ButtonOnWorkDisabled = false;
                ButtonOnWorkEnabled = true;
            }
        }

       
       ////////////////////////////////////////// Обработка изображений  ///////////////////////////////
       
        VideoCapture videoCapture;
        private string _ButtonText;
        public string ButtonText
        {
            get { return _ButtonText; }
            set
            {
                _ButtonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private int step;
        private void Initial()
        {  
            if (videoCapture != null)
            {
                videoCapture.Stop();
                videoCapture.Dispose();
                videoCapture = null;
            }
            videoCapture = new VideoCapture(GetCamera());
            videoCapture.ImageGrabbed += VideoCapture_ImageGrabbed;
            videoCapture.Start();         
         
        }

        private int GetCamera()
        {
            string _file = "CameraOption.txt";
            int _camidx = 0;
            if (!File.Exists(_file))
            {
                using(StreamWriter sw=new StreamWriter(_file))
                {
                    sw.WriteLine("0");
                }
            }
            using(StreamReader sr=new StreamReader(_file))
            {
                _camidx = int.Parse(sr.ReadLine());
            }
            return _camidx;
        }
        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            Mat frame = new Mat();
            //frame = videoCapture.QueryFrame(); // запрос нового кадра
            videoCapture.Retrieve(frame);
            Image getImages = frame.ToImage<Bgr, byte>().ToBitmap();
            GetBitmapImage.Dispatcher.Invoke(new Action(() => { Convert(getImages); }));
            ImagePath = getImages;
            step++;
            ButtonText = $"{step}";
        }

        private byte[] GetFoto()
        {
            Mat frame = new Mat();
            videoCapture.Retrieve(frame);
            return frame.ToImage<Bgr, byte>().Bytes;
        }

        // Convert Image to BitmapImage
        public void Convert(Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                GetBitmapImage = bitmapImage;
            }
        }

        private Image _ImagePath;
        public Image ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private BitmapImage _GetBitmapImage = new BitmapImage();
        public BitmapImage GetBitmapImage
        {
            get { return _GetBitmapImage; }
            set
            {
                _GetBitmapImage = value;
                OnPropertyChanged(nameof(GetBitmapImage));
            }
        }


        private void StopCapture()
        {
            videoCapture.Stop(); 
            videoCapture.Dispose();
        }       

        // Свойство для хранения индекса по ContentUserTimesList
        private int _listUserTimeIndex;
        public int ListUserTimeIndex
        {
            get { return _listUserTimeIndex; }
            set
            {
                _listUserTimeIndex = value;
                OnPropertyChanged(nameof(ListUserTimeIndex));
            }
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

        private void SetImageUserTime()
        {
            if (ListUserTimeIndex > -1)
            {
                byte[] bytes = ContentUserTimesList[ListUserTimeIndex].Photo;
                GetTimeImage = ImageConvert.ConvertByteToBitmapImage(bytes);
            }
        }
    }
}
