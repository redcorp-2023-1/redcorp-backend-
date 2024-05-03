using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace RedcorpCenter.Domain
{
    public class EmployeeDomain : IEmployeeDomain
    {
        private IEmployeeInfraestructure _employeeInfraestructure;
        private IEncryptDomain _encryptDomain;
        private ITokenDomain _tokenDomain;
        public EmployeeDomain(IEmployeeInfraestructure employeeInfraestructure, IEncryptDomain encryptDomain, ITokenDomain tokenDomain)
        {
            _employeeInfraestructure = employeeInfraestructure;
            _encryptDomain = encryptDomain;
            _tokenDomain = tokenDomain;
        }



        public async Task<bool> SaveAsync(Employee employee)
        {
            if (!this.IsValidData(employee.Name, employee.last_name)) throw new Exception("La longitud del nombre o apellido es menor a 3 caracteres");
            if (employee.Name.Length > 20) throw new Exception("La longitud del nombre es mayor a 20 caracteres");

            return await _employeeInfraestructure.SaveAsync(employee);
        }

        public bool update(int id, string name, string last_name, string email, string area, string cargo)
        {
            if (!this.IsValidData(name, last_name)) throw new Exception("La longitud del nombre o apellido es inválida");
            return _employeeInfraestructure.update(id, name, last_name, email, area, cargo);
        }
        
        public async Task<bool> UpdateAsync(int id, Employee employee)
        {
            if (!this.IsValidData(employee.Name, employee.last_name)) throw new Exception("La longitud de tu nombre es inválida");
            return await _employeeInfraestructure.UpdateAsync(id, employee);
        }

        public bool delete(int id)
        {
            return _employeeInfraestructure.delete(id);
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            return await _employeeInfraestructure.DeleteAsync(id);
        }


        private bool IsValidData(string name, string last_name)
        {
            if (name.Length < 3 || last_name.Length < 3) return false;
            return true;
        }

        public async Task<int> Signup(Employee employee)
        {
            employee.password = _encryptDomain.Encrypt(employee.password);

            if (!this.IsValidData(employee.Name, employee.last_name))
            {
                return 0;
            }

            return await _employeeInfraestructure.Signup(employee);
        }



        public async Task<string> Login(Employee employee)
        {
            var foundUser = await _employeeInfraestructure.GetByEmailAsync(employee.email);

            if (_encryptDomain.Encrypt(employee.password) == foundUser.password)
            {
                return _tokenDomain.GenerateJwt(foundUser.email);
            }

            throw new ArgumentException("Email o contraseña inválida");
        }

        public async Task<Employee> GetByEmail(string email)
        {
            return await _employeeInfraestructure.GetByEmailAsync(email);
        }
    }
}