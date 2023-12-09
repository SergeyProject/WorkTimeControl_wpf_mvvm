using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services;
using WorkTimeControl.DAL.Entities;
using WorkTimeControl.DAL.Repositories;

namespace WorkTimeControl.Client
{
    public partial class Form1 : Form
    {
        private List<UserDTO> usersList = new List<UserDTO>();
        private List<UserTimeDTO> timeList = new List<UserTimeDTO>();
        private Guid _id;
        public Form1()
        {
            InitializeComponent();
            LoadList();
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _id = usersList[listBox1.SelectedIndex].Id;
            Text = $"{_id} / {usersList[listBox1.SelectedIndex].Name}";
            LoadUserTime(_id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            LoadList();
        }

        private void LoadList()
        {
            listBox1.Items.Clear();
            usersList.Clear();
            Services services = new Services();
            usersList = services.GetAllUsers3();
            foreach (UserDTO user in services.GetAllUsers3())
            {
                listBox1.Items.Add(user.Name);
            }
        }

        private void LoadUserTime(Guid id)
        {
            ServiceDTO service = new ServiceDTO();
            listBox2.Items.Clear();
            timeList.Clear();
            foreach (UserTimeDTO userTimes in service.UserTimeService().GetUserTimes(id))
            {
                timeList.Add(userTimes);
                if (userTimes.IsOnWork)
                {
                    listBox2.Items.Add($"Прибыл на работу:\t{userTimes.DateTimes.ToShortTimeString()}");
                }
                else
                {
                    listBox2.Items.Add($"Убыл с работы:\t{userTimes.DateTimes.ToShortTimeString()}");
                }
            }
            ButtonEnable(timeList);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(_id);
            form3.ShowDialog();
            LoadList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServiceDTO service = new ServiceDTO();
            service.UserTimeService().Delete(_id);
            service.UserService().Delete(_id);
            LoadList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            UserTimeDTO userTimeDTO = new UserTimeDTO()
            {
                DateTimes = DateTime.Now,
                Descript = "Прибытие",
                IsOnWork = true,
                Photo = null,
                UserId = _id
            };
            serviceDTO.UserTimeService().TimeStampCreate(userTimeDTO);
            LoadUserTime(_id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            UserTimeDTO userTimeDTO = new UserTimeDTO()
            {
                DateTimes = DateTime.Now,
                Descript = "Убытие",
                IsOnWork = false,
                Photo = null,
                UserId = _id
            };
            serviceDTO.UserTimeService().TimeStampCreate(userTimeDTO);
            LoadUserTime(_id);
        }

        private void ButtonEnable(List<UserTimeDTO> userTimes)
        {
            int cnt = userTimes.Count;
            if (cnt == 0)
            {
                button2.Enabled = true;
                button3.Enabled = false;

            }
            else
            {
                if (userTimes[cnt - 1].IsOnWork)
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }

        }
    }
}