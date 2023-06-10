using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Infraestructure
{
    public interface IProjectInfraestructure
    {
        Project GetById(int id);
        public Task<bool> SaveAsync(Project project);
        public bool update(int id, Project project);
        public bool delete(int id);
        List<Project> GetAll();
    }
}

