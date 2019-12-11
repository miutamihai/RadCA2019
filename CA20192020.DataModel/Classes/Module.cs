using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA20192020.DataModel.Classes
{
    public class Module
    {
        [Key]
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string Description { get; set; }

    }
}
