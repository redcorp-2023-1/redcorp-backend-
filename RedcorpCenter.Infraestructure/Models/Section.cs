using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Section_Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        
    }
}
