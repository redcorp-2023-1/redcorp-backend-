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
        public bool update(int id, string name);
        public bool delete(int id);
        Task<List<Employee>> GetAllAsync();
    }
}
