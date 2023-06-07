using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RedcorpCenter.Infraestructure
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
                _redcorpCenterDBContext.SectionsAndEmployees.Add(sectionAndEmployees);
                _redcorpCenterDBContext.SaveChanges();
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
    }
}
