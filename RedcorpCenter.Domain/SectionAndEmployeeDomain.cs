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
    }
}
