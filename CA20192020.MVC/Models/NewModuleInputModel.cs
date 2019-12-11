using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA20192020.MVC.Models
{
    public class NewModuleInputModel
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string Description { get; set; }
        public string LecturerFullName { get; set; }
        public int DeliveryDay { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int Duration { get; set; }
    }
}