using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure.Models
{
    public class SectionAndEmployee
    {
        public int Id { get; set; }
        public int Section_Id { get; set; }
        public int Employees_Id { get; set; }
        public bool IsActive { get; set; }
    }
}
