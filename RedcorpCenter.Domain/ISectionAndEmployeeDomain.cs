using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public interface ISectionAndEmployeeDomain
    {
        public bool Save(SectionAndEmployee sectionAndEmployee);
        public bool update(int id, int Section_id, int Employee_id);
        public bool delete(int id);
    }
}
