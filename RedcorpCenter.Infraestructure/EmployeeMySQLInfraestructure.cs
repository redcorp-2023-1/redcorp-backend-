using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
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
            try
            {
                return await _redcorpCenterDBContext.Employees.Where(employee => employee.IsActive).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener todos los empleados de la base de datos", e);
            }

        }

        public async Task<bool> SaveAsync(Employee employee)
        {
            try
            {
                if(employee.cargo == "Supervisor")
                {
                    employee.Roles = "admin";
                }
                else
                {
                    employee.Roles = "user";
                }

                employee.IsActive = true;   
                await _redcorpCenterDBContext.Employees.AddAsync(employee);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Error al guardar el nuevo usuario en la base de datos", exception);
            }
            
        }

        public bool update(int id, string name, string last_name, string email, string area, string cargo)
        {
            Employee _employee = _redcorpCenterDBContext.Employees.Find(id);
            _employee.Name = name;
            _employee.last_name = last_name;
            _employee.email = email;

            _redcorpCenterDBContext.Employees.Update(_employee);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
        
        public async Task<bool> UpdateAsync(int id, Employee employee)
        {
            try
            {
                Employee _employee = await _redcorpCenterDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (_employee == null)
                    return false;

                _employee.Name = employee.Name;
                _employee.last_name = employee.last_name;
                _employee.email = employee.email;
                _employee.area = employee.area;
                _employee.cargo = employee.cargo;
                _employee.photo = employee.photo;

                _redcorpCenterDBContext.Employees.Update(_employee);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar el usuario en la base de datos.", e);
            }

        }

        public bool delete(int id)
        {
            Employee employee = _redcorpCenterDBContext.Employees.Find(id);
            employee.IsActive = false;
            _redcorpCenterDBContext.Employees.Update(employee);
            _redcorpCenterDBContext.SaveChanges();
            return true;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Employee employee = await _redcorpCenterDBContext.Employees.FindAsync(id);
                employee.IsActive = false;
                _redcorpCenterDBContext.Employees.Update(employee);
                await _redcorpCenterDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al borrar al usuario en la base de datos", e);
            }

        }

        public Employee GetById(int id)
        {
            return _redcorpCenterDBContext.Employees.Find(id);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                return await _redcorpCenterDBContext.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener al usuario con el id especificado en la base de datos.", ex);
            }

        }
        
        public async Task<Employee> GetByLogin(string email, string password)
        {
            try
            {
                Employee employee = await _redcorpCenterDBContext.Employees.FirstOrDefaultAsync(x => x.email == email && x.password == password);
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener login.", ex);
            }
        }

        public async Task<int> Signup(Employee employee)
        {
            try
            {

                if (employee.cargo == "Supervisor")
                {
                    employee.Roles = "admin";
                }
                else
                {
                    employee.Roles = "user";
                }

                _redcorpCenterDBContext.Employees.Add(employee);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return employee.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar empleado.", ex);
            }
        }

        

        public async Task<Employee> GetByEmailAsync(string email)
        {
            try
            {
                return await _redcorpCenterDBContext.Employees.FirstOrDefaultAsync(e => e.email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el email del empleado.", ex);
            }
        }
    }
}
