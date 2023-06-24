using RedcorpCenter.Infra.Models;

namespace RedcorpCenter.Domain
{
    public interface ISectionAndEmployeeDomain
    {
        public bool Save(SectionAndEmployee sectionAndEmployee);
        public bool update(int id, int Section_id, int Employee_id);
        public bool delete(int id);
    }
}
