using RedcorpCenter.Infra;
using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public class SectionDomain : ISectionDomain
    {
        public ISectionInfraestructure _sectionInfraestructure;

        public SectionDomain(ISectionInfraestructure sectionInfraestructure)
        {
            _sectionInfraestructure = sectionInfraestructure;
        }

        public bool delete(int id)
        {
            return _sectionInfraestructure.delete(id);
        }

        public bool Save(Section section)
        {
            if (!this.IsValidSyntaxis(section.Section_Name, section.Description)) throw new Exception("The syntaxis is incorrect");

            return _sectionInfraestructure.Save(section);
        }

        public bool update(int id, string section_name, string description)
        {

            if (!this.IsValidSyntaxis(section_name, description)) throw new Exception("The syntaxis is incorrect");
            return _sectionInfraestructure.update(id, section_name, description);
        }

        private bool IsValidSyntaxis(string Section_Name, string Description)
        {
            if (Section_Name.Length < 5) return false;
            ;
            if (Description.Length < 10) return false;


            return true;
        }


    }
}
