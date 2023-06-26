using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure
{
    public interface IEmployeeInfraestructure
    {
        Employee GetById(int id);
        public Task<bool> SaveAsync(Employee employee);
        public bool update(int id, string name, string last_name, string email, string area, string cargo);
        public bool delete(int id);

        public Task<Employee> GetByEmail (string email);
        public Task<int> Signup  (Employee employee);
        Employee GetByLogin(string email, string password);
        Task<List<Employee>> GetAllAsync();
    }
}
