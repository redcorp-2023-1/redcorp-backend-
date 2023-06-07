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


        public bool Save(SectionsAndEmployees sectionAndEmployee)
        {
            return _sectionAndEmployeeInfraestructure.Save(sectionAndEmployee);
        }

        public bool update(int id, int Section_id, int Employee_id)
        {
            return _sectionAndEmployeeInfraestructure.update(id, Section_id, Employee_id);  
        }
        public bool delete(int id)
        {
            return _sectionAndEmployeeInfraestructure.delete(id);
        }
    }
}
