using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure
{
    public class EmployeeMySQLInfraestructure :IEmployeeInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public EmployeeMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext= redcorpCenterDBContext;
        }


        public async Task<List<Employee>> GetAllAsync()
        {


            return await _redcorpCenterDBContext.Employees.Where(employee => employee.IsActive).ToListAsync();

        }
        public Employee GetById(int id)
        {
            return _redcorpCenterDBContext.Employees.Find(id);
        }


        public bool Save(Employee employee)
        {
            
            
                try
                {
                    _redcorpCenterDBContext.Employees.Add(employee);
                    _redcorpCenterDBContext.SaveChanges();
 
                }
                catch (Exception exception)
                {

                    throw;
                }
   
            return true; 
        }
        public bool update(int id, string name, string last_name, string email)
        {
            Employee _employee = _redcorpCenterDBContext.Employees.Find(id);
            _employee.Name = name;
            _employee.last_name = last_name;
            _employee.email = email;

            _redcorpCenterDBContext.Employees.Update(_employee);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }

        public bool delete(int id)
        {
            Employee employee = _redcorpCenterDBContext.Employees.Find(id);

            employee.IsActive = false;

            _redcorpCenterDBContext.Employees.Update(employee);

            //_learningCenterDbContext.Tutorials.Remove(tutorial); Eliminacion física

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }

        
    }
}
