using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeControl.BLL.Services;

namespace WorkTimeControl.WpfClient.DataModels
{
    partial class DataService
    {

        public void GetUserTimeList(Guid id)
        {
            ServiceDTO serviceDTO = new ServiceDTO();
            var item = serviceDTO.UserTimeService().GetUserTimes(id);
        }
    }
}
