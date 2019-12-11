using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA20192020.MVC.Models
{
    public class AttendanceList
    {
        public int ID { get; set; }
        public string ModuleCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}