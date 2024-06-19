using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Net;

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
            try
            {
                return await _redcorpCenterDBContext.SectionsAndEmployees.Where(SE => SE.IsActive).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los SectionAndEmployee de la base de datos.", e);
            }

        }

        public async Task<SectionAndEmployee> GetByIdAsync(int id)
        {
            try
            {
                return await _redcorpCenterDBContext.SectionsAndEmployees.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el SectionAndEmployee con el id especificado en la base de datos.", ex);
            }

        }

        public async Task<bool> SaveAsync(SectionAndEmployee sectionAndEmployees)
        {
            try
            {
                if(await existsSectionIdAndEmployeeId(sectionAndEmployees))
                {
                    await _redcorpCenterDBContext.SectionsAndEmployees.AddAsync(sectionAndEmployees);
                    await _redcorpCenterDBContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar SectionAndEmployee en la base de datos.",ex);
            }

        }

        public async Task<bool> UpdateAsync(int id, int Section_Id, int Employee_Id)
        {
            try
            {
                SectionAndEmployee _sectionsAndEmployees = await _redcorpCenterDBContext.SectionsAndEmployees.FindAsync(id);
                _sectionsAndEmployees.Section_Id = Section_Id;
                _sectionsAndEmployees.Employees_Id = Employee_Id;

                _redcorpCenterDBContext.SectionsAndEmployees.Update(_sectionsAndEmployees);
                await _redcorpCenterDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar SectionAndEmployee en la base de datos.",e);
            }
 
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                SectionAndEmployee sectionsAndEmployees = await _redcorpCenterDBContext.SectionsAndEmployees.FindAsync(id);
                sectionsAndEmployees.IsActive = false;
                _redcorpCenterDBContext.SectionsAndEmployees.Update(sectionsAndEmployees);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar SectionAndEmployee en la base de datos.", e);
            }
        }

        public async Task<bool> existsSectionIdAndEmployeeId(SectionAndEmployee sectionAndEmployee)
        {
            bool employeeExists = await _redcorpCenterDBContext.Employees.AnyAsync(e => e.Id == sectionAndEmployee.Employees_Id);
            bool sectionExists = await _redcorpCenterDBContext.Sections.AnyAsync(s => s.Id == sectionAndEmployee.Section_Id);

            return employeeExists && sectionExists;
        }
        public async Task<List<Employee>> GetEmployeesBySectionId(int sectionId)
        {
            try
            {
                List<int> employeeIds = await _redcorpCenterDBContext.SectionsAndEmployees
                    .Where(se => se.Section_Id == sectionId)
                    .Select(se => se.Employees_Id)
                    .ToListAsync();

                List<Employee> employees = await _redcorpCenterDBContext.Employees
                    .Where(e => employeeIds.Contains(e.Id))
                    .ToListAsync();

                return employees;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener empleados con el id especificado en la base de datos.", e);
            }

        }
        public async Task<List<Models.Section>> GetSectionsByEmployeeId(int employeeId)
        {
            try
            {
                List<Models.Section> sections = new List<Models.Section>();

                List<SectionAndEmployee> sectionAndEmployees = await _redcorpCenterDBContext.SectionsAndEmployees
                    .Where(se => se.Employees_Id == employeeId)
                    .ToListAsync();

                foreach (SectionAndEmployee sectionAndEmployee in sectionAndEmployees)
                {
                    Models.Section section = await _redcorpCenterDBContext.Sections.FirstOrDefaultAsync(s => s.Id == sectionAndEmployee.Section_Id);
                    if (section != null)
                    {
                        sections.Add(section);
                    }
                }

                return sections;
            }
            catch (Exception e)
            {
                throw new Exception("Error al buscar Section con el id especificado en la base de datos", e);
            }

        }
    }
}
