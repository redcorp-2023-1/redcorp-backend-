using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infra.Context;
using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public class SectionAndEmployeeMySQLInfraestructure : ISectionAndEmployeeInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public SectionAndEmployeeMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext = redcorpCenterDBContext;
        }

        public async Task<List<SectionAndEmployee>> GetAllAsync()
        {
            return await _redcorpCenterDBContext.SectionsAndEmployees.Where(SE => SE.IsActive).ToListAsync();
        }

        public SectionAndEmployee GetById(int id)
        {
            return _redcorpCenterDBContext.SectionsAndEmployees.Find(id);
        }

        public bool Save(SectionAndEmployee sectionAndEmployees)
        {
            try
            {
                if (existsSectionIdAndEmployeeId(sectionAndEmployees))
                {
                    _redcorpCenterDBContext.SectionsAndEmployees.Add(sectionAndEmployees);
                    _redcorpCenterDBContext.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public bool update(int id, int Section_Id, int Employee_Id)
        {
            SectionAndEmployee _sectionsAndEmployees = _redcorpCenterDBContext.SectionsAndEmployees.Find(id);
            _sectionsAndEmployees.Section_Id = Section_Id;
            _sectionsAndEmployees.Employees_Id = Employee_Id;

            _redcorpCenterDBContext.SectionsAndEmployees.Update(_sectionsAndEmployees);
            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
        public bool delete(int id)
        {
            SectionAndEmployee sectionsAndEmployees = _redcorpCenterDBContext.SectionsAndEmployees.Find(id);
            sectionsAndEmployees.IsActive = false;
            _redcorpCenterDBContext.SectionsAndEmployees.Update(sectionsAndEmployees);
            _redcorpCenterDBContext.SaveChanges();
            return true;
        }

        public bool existsSectionIdAndEmployeeId(SectionAndEmployee sectionAndEmployee)
        {
            bool employeeExists = _redcorpCenterDBContext.Employees.Any(e => e.Id == sectionAndEmployee.Employees_Id);
            bool sectionExists = _redcorpCenterDBContext.Sections.Any(s => s.Id == sectionAndEmployee.Section_Id);

            return employeeExists && sectionExists;
        }
        public List<Employee> GetEmployeesBySectionId(int sectionId)
        {
            List<int> employeeIds = _redcorpCenterDBContext.SectionsAndEmployees
                .Where(se => se.Section_Id == sectionId)
                .Select(se => se.Employees_Id)
                .ToList();

            List<Employee> employees = _redcorpCenterDBContext.Employees
                .Where(e => employeeIds.Contains(e.Id))
                .ToList();

            return employees;
        }
        public List<Models.Section> GetSectionsByEmployeeId(int employeeId)
        {
            List<Models.Section> sections = new List<Models.Section>();

            List<SectionAndEmployee> sectionAndEmployees = _redcorpCenterDBContext.SectionsAndEmployees
                .Where(se => se.Employees_Id == employeeId)
                .ToList();

            foreach (SectionAndEmployee sectionAndEmployee in sectionAndEmployees)
            {
                Models.Section section = _redcorpCenterDBContext.Sections.FirstOrDefault(s => s.Id == sectionAndEmployee.Section_Id);
                if (section != null)
                {
                    sections.Add(section);
                }
            }

            return sections;
        }
    }
}
