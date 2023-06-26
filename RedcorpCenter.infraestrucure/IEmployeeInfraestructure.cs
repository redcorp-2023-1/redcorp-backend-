using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public interface IEmployeeInfraestructure
    {
        Employee GetById(int id);
        public bool Save(Employee employee);
        public bool update(int id, string name, string last_name, string email, string area, string cargo);
        public bool delete(int id);

        public Employee GetByEmail(string email);
        public int Signup(Employee employee);
        Employee GetByLogin(string email, string password);
        Task<List<Employee>> GetAllAsync();
    }
}
