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
        public Task<bool> SaveAsync(Employee employee);
        public bool update(int id, string name, string last_name, string email, string area, string cargo);
        public bool delete(int id);

        public Task<int> Signup(Employee employee);

        Task<string> Login(Employee employee);

        Task<Employee> GetByEmail(string username);
        public Task<bool> UpdateAsync(int id, Employee employee);
        public Task<bool> DeleteAsync(int id);

    }
}
