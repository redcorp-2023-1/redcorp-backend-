using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public interface IEmployeeDomain
    {
        public bool Save(Employee employee);
        public bool update(int id, string name, string last_name, string email, string area, string cargo);
        public bool delete(int id);

        public int Signup(Employee employee);

        public Employee LogIn(string email,string password);
    }
}
