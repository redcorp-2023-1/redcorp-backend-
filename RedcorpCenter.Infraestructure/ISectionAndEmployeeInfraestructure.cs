using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure
{
    public interface ISectionAndEmployeeInfraestructure
    {
        Task<SectionAndEmployee> GetByIdAsync(int id);
        public Task<bool> SaveAsync(SectionAndEmployee sectionAndEmployees);
        public Task<bool> UpdateAsync(int id, int Section_Id, int Employee_Id);
        public Task<bool> DeleteAsync(int id);

        public Task<bool> existsSectionIdAndEmployeeId(SectionAndEmployee sectionAndEmployee);
        public Task<List<Employee>> GetEmployeesBySectionId(int sectionId);
        public Task<List<Section>> GetSectionsByEmployeeId(int employeeId);
        Task<List<SectionAndEmployee>> GetAllAsync();

    }
}
