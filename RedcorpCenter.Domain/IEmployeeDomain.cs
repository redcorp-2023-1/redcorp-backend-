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
        public bool update(int id, string name);
        public bool delete(int id);
    }
}
