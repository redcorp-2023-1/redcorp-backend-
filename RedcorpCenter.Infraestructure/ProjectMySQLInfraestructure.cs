using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Infraestructure
{
    public class ProjectMySQLInfraestructure : IProjectInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public ProjectMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext = redcorpCenterDBContext;
        }

        public List<Project> GetAll()
        {
            return _redcorpCenterDBContext.Projects.Where(project => project.IsActive).ToList();
        }

        public Project GetById(int id)
        {
            return _redcorpCenterDBContext.Projects.Find(id);
        }

        public async Task<bool> SaveAsync(Project project)
        {
            try
            {
                project.IsActive = true;
                await _redcorpCenterDBContext.Projects.AddAsync(project);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred while saving project: {exception.Message}");
                return false;
            }
        }

        public bool update(int id, Project project)
        {
            try
            {
                Project _project = _redcorpCenterDBContext.Projects.Find(id);
                _project.Name = project.Name;
                _project.Description = project.Description;
                _project.FinalDate = project.FinalDate;
                _project.State = project.State;

                _redcorpCenterDBContext.Projects.Update(_project);
                _redcorpCenterDBContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred while updating project: {exception.Message}");
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                Project project = _redcorpCenterDBContext.Projects.Find(id);
                project.IsActive = false;
                _redcorpCenterDBContext.Projects.Update(project);
                _redcorpCenterDBContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred while deleting project: {exception.Message}");
                return false;
            }
        }
    }
}