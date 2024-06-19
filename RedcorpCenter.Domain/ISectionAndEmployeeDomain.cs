using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Domain
{
    public interface ISectionAndEmployeeDomain
    {
        public Task<bool> SaveAsync(SectionAndEmployee sectionAndEmployee);
        public Task<bool> UpdateAsync(int id, int Section_id, int Employee_id);
        public Task<bool> DeleteAsync(int id);
    }
}
