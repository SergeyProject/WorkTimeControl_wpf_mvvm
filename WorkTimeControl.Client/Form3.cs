using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTimeControl.BLL.DTO;
using WorkTimeControl.BLL.Services;

namespace WorkTimeControl.Client
{
    public partial class Form3 : Form
    {
        private readonly Guid _id;
        public Form3(Guid id)
        {
            InitializeComponent();
            _id = id;
            LoadName();
        }

        private void LoadName()
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            textBox1.Text = serviceDTO.UserService().GetUserById(_id).Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            UserDTO userDTO = new UserDTO() {Id=_id, Name = textBox1.Text };
            serviceDTO.UserService().UpdateUser(_id, userDTO);
            this.Close();
        }
    }
}
