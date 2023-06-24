using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id_Employee { get; set; }
        public int Id_Project { get; set; }
        public int Id_Task { get; set; }

        public bool IsActive { get; set; }
    }
}
