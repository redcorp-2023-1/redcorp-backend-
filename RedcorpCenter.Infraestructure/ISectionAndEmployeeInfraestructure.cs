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
        SectionsAndEmployees GetById(int id);
        public bool Save (SectionsAndEmployees sectionAndEmployees);
        public bool update(int id, int Section_Id,int Employee_Id);

        public bool delete(int id);

        Task<List<SectionsAndEmployees>>GetAllAsync();

    }
}
