﻿using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infra.Context;
using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public class EmployeeMySQLInfraestructure : IEmployeeInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public EmployeeMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext = redcorpCenterDBContext;
        }


        public async Task<List<Employee>> GetAllAsync()
        {
            return await _redcorpCenterDBContext.Employees.Where(employee => employee.IsActive).ToListAsync();
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

        public bool delete(int id)
        {
            Employee employee = _redcorpCenterDBContext.Employees.Find(id);

            employee.IsActive = false;

            _redcorpCenterDBContext.Employees.Update(employee);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }

        public Employee GetById(int id)
        {
            return _redcorpCenterDBContext.Employees.Find(id);
        }

        public Employee GetByLogin(string email, string password)
        {
            Employee employee = _redcorpCenterDBContext.Employees.Where(x => x.email == email && x.password == password).FirstOrDefault();

            return employee;
        }

        public int Signup(Employee employee)
        {
            _redcorpCenterDBContext.Employees.Add(employee);
            _redcorpCenterDBContext.SaveChanges();
            return employee.Id;
        }

        public Employee GetByEmail(string email)
        {
            Employee employee = _redcorpCenterDBContext.Employees.SingleOrDefault(e => e.email == email);

            return employee;
        }
    }
}
