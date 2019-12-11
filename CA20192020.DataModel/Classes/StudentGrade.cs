using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA20192020.DataModel.Classes
{
    public class StudentGrade
    {
        [Key]
        public int ID { get; set; }
        public int AssessmentID { get; set; }
        [ForeignKey("AssessmentID")]
        public virtual ModuleAssessment ModuleAssessment { get; set; }
        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        public float Grade { get; set; }
    }
}
