using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public interface ISectionAndEmployeeInfraestructure
    {
        SectionAndEmployee GetById(int id);
        public bool Save(SectionAndEmployee sectionAndEmployees);
        public bool update(int id, int Section_Id, int Employee_Id);
        public bool delete(int id);

        public bool existsSectionIdAndEmployeeId(SectionAndEmployee sectionAndEmployee);
        public List<Employee> GetEmployeesBySectionId(int sectionId);
        public List<Section> GetSectionsByEmployeeId(int employeeId);
        Task<List<SectionAndEmployee>> GetAllAsync();

    }   
}
