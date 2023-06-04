using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Infraestructure;

public class ProjectMySQLInfraestructure : IProjectInfraestructure
{
    
    private RedcorpCenterDBContext _redcorpCenterDBContext;

    public ProjectMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
    {
        _redcorpCenterDBContext= redcorpCenterDBContext;
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
            _redcorpCenterDBContext.Projects.Add(project);
            await _redcorpCenterDBContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw;
        }
        return true;
    }

    public bool update(int id, Project project)
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



    public bool delete(int id)
    {
        Project project = _redcorpCenterDBContext.Projects.Find(id);
        
        project.IsActive = false;
        
        _redcorpCenterDBContext.Projects.Update(project);
        
        _redcorpCenterDBContext.SaveChanges();

        return true;
    }
}