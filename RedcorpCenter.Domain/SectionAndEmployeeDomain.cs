using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public class SectionAndEmployeeDomain : ISectionAndEmployeeDomain
    {
        public ISectionAndEmployeeInfraestructure _sectionAndEmployeeInfraestructure;

        public SectionAndEmployeeDomain(ISectionAndEmployeeInfraestructure sectionAndEmployeeInfraestructure)
        {
            _sectionAndEmployeeInfraestructure = sectionAndEmployeeInfraestructure;
        }


        public async Task<bool> SaveAsync(SectionAndEmployee sectionAndEmployee)
        {
            //Validation
            if (!ValidIdSection(sectionAndEmployee.Section_Id)) throw new Exception("El id del Section es invalido");
            if (!ValidIdEmployee(sectionAndEmployee.Employees_Id)) throw new Exception("El id del Employee es invalido");
            
            return await _sectionAndEmployeeInfraestructure.SaveAsync(sectionAndEmployee);
        }

        public async Task<bool> UpdateAsync(int id, int Section_id, int Employee_id)
        {
            return await _sectionAndEmployeeInfraestructure.UpdateAsync(id, Section_id, Employee_id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _sectionAndEmployeeInfraestructure.DeleteAsync(id);
        }

        private bool ValidIdSection(int Section_Id)
        {
            if (Section_Id < 1) return false;

            return true;
        }
        
        private bool ValidIdEmployee(int Employee_Id)
        {
            if (Employee_Id < 1) return false;

            return true;
        }
    }
}
