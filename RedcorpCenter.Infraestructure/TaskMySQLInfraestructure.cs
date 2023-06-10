using RedcorpCenter.Infraestructure.Context;
using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Infraestructure;

public class TaskMySQLInfraestructure : ITaskInfraestructure
{
    
    private RedcorpCenterDBContext _redcorpCenterDBContext;

    public TaskMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
    {
        _redcorpCenterDBContext= redcorpCenterDBContext;
    }
    
    public List<Task> GetAll()
    {
        return _redcorpCenterDBContext.Tasks.Where(project => project.IsActive).ToList();
    }
    
    public Task GetById(int id)
    {
        return _redcorpCenterDBContext.Tasks.Find(id);
    }

    public async Task<bool> SaveAsync(Task task)
    {
        try
        {
            _redcorpCenterDBContext.Tasks.Add(task);
            await _redcorpCenterDBContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw;
        }
        return true;
    }

    public bool update(int id, Task task)
    {
        Task _task = _redcorpCenterDBContext.Tasks.Find(id);
        _task.Name = task.Name;
        _task.Description = task.Description;
        _task.FinalDate = task.FinalDate;
        _task.IsCompleted = task.IsCompleted;

        _redcorpCenterDBContext.Tasks.Update(_task);

        _redcorpCenterDBContext.SaveChanges();

        return true;
    }

    public bool delete(int id)
    {
        Task task = _redcorpCenterDBContext.Tasks.Find(id);
        
        task.IsActive = false;
        
        _redcorpCenterDBContext.Tasks.Update(task);
        
        _redcorpCenterDBContext.SaveChanges();

        return true;
    }
}