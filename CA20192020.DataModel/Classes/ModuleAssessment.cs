using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA20192020.DataModel.Classes
{
    public class ModuleAssessment
    {
        [Key]
        public int ID { get; set; }
        public int ModuleID { get; set; }
        [ForeignKey("ID")]
        public virtual Module Module { get; set; }
        public float PercentageAllocation { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
