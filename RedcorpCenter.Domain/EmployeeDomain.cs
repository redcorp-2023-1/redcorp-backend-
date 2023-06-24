using RedcorpCenter.Infra.Models;
using RedcorpCenter.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace RedcorpCenter.Domain
{
    public class EmployeeDomain :IEmployeeDomain
    {
        public IEmployeeInfraestructure _employeeInfraestructure;
        private IEncryptDomain _encryptDomain;
        public EmployeeDomain(IEmployeeInfraestructure employeeInfraestructure, IEncryptDomain encryptDomain)
        {
            _employeeInfraestructure = employeeInfraestructure;
            _encryptDomain = encryptDomain;
        }



        public bool Save(Employee employee)
        {
            if (!this.IsValidData(employee.Name, employee.last_name)) throw new Exception("The length of your name and lastname is invalid(>3)");
            if (employee.Name.Length > 20) throw new Exception("the name is more than 20");

            return _employeeInfraestructure.Save(employee);
        }

        public bool update(int id, string name, string last_name, string email, string area, string cargo)
        {
            if (!this.IsValidData(name,last_name)) throw new Exception("The length of your name is invalid");
            return _employeeInfraestructure.update(id, name, last_name, email, area, cargo);
        }

        public bool delete(int id)
        {
            return _employeeInfraestructure.delete(id);
        }


        private bool IsValidData(string name, string last_name)
        {
            if (name.Length < 3 || last_name.Length <3) return false;
            return true;
        }

        public int Signup(Employee employee)
        {
            employee.password = _encryptDomain.Encrypt(employee.password);

            if (!this.IsValidData(employee.Name, employee.last_name))
            {
                return 0;
            }

            return _employeeInfraestructure.Signup(employee);   
        }



        public Employee LogIn(string email, string password)
        {
            var foundUser = _employeeInfraestructure.GetByEmail(email);

            if (foundUser != null && _encryptDomain.Encrypt(password) == foundUser.password)
            {
                return foundUser;
            }

            return null;
        }
    }
}
