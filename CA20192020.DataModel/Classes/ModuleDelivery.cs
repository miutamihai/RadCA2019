using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA20192020.DataModel.Classes
{
    public class ModuleDelivery
    {
        [Key]
        public int ID { get; set; }
        public int ModuleID { get; set; }
        [ForeignKey("ModuleID")]
        public virtual Module Module { get; set; }
        public int LecturerID { get; set; }
        [ForeignKey("LecturerID")]
        public virtual Lecturer Lecturer { get; set; }
        public int DeliveryDay { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int Duration { get; set; }
    }
}
