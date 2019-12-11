using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA20192020.MVC.Models
{
    public class CreateAttendanceSheet
    {
        public int ID { get; set; }
        public string ModuleCode { get; set; }
        public string StudentID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
    }
}