using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string last_name { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public string area { get; set; }
        public string cargo { get; set; }

        public string photo { get; set; }
        public bool IsActive { get; set; }


    }
}
