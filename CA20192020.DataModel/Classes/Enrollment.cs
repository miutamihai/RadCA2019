using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA20192020.DataModel.Classes
{
    public class Enrollment
    {
        [Key]
        public int ID { get; set; }
        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        public int ModuleID { get; set; }
        [ForeignKey("ModuleID")]
        public virtual Module Module { get; set; }
    }
}
