﻿using RedcorpCenter.Infraestructure.Models;
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
            if (!this.IsValidData(employee.Name, employee.last_name)) throw new Exception("The length of your name and lastname is invalid(>3)");
            if (employee.Name.Length > 20) throw new Exception("the name is more than 20");

            return await _employeeInfraestructure.SaveAsync(employee);
        }

        public bool update(int id, string name, string last_name, string email, string area, string cargo)
        {
            if (!this.IsValidData(name, last_name)) throw new Exception("The length of your name is invalid");
            return _employeeInfraestructure.update(id, name, last_name, email, area, cargo);
        }

        public bool delete(int id)
        {
            return _employeeInfraestructure.delete(id);
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
            var foundUser = await _employeeInfraestructure.GetByEmail(employee.email);

            if (_encryptDomain.Encrypt(employee.password) == foundUser.password)
            {
                return _tokenDomain.GenerateJwt(foundUser.email);
            }

            throw new ArgumentException("Invalid email or password");
        }

        public async Task<Employee> GetByEmail(string email)
        {
            return await _employeeInfraestructure.GetByEmail(email);
        }
    }
}