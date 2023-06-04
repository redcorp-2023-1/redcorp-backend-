using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Infraestructure
{
    public interface IProjectInfraestructure
    {
        Project GetById(int id);
        public Task<bool> SaveAsync(Project employee);
        public bool update(int id, Project employee);
        public bool delete(int id);
        List<Project> GetAll();
    }
}

