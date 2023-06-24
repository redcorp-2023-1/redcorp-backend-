using RedcorpCenter.Infraestructure;
using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Domain;

public class TaskDomain : ITaskDomain
{
    public ITaskInfraestructure _taskInfraestructure;

    public TaskDomain(ITaskInfraestructure taskInfraestructure)
    {
        _taskInfraestructure = taskInfraestructure;
    }
    
    public async Task<bool> SaveAsync(Task task)
    {
        if (!this.IsValidData(task)) return false;

        return await _taskInfraestructure.SaveAsync(task);
    }

    public bool update(int id, Task task)
    {
        if (!this.IsValidData(task)) return false;

        return _taskInfraestructure.update(id, task);
    }

    public bool delete(int id)
    {
        return _taskInfraestructure.delete(id);
    }
    
    private bool IsValidData(Task task)
    {
        if (!this.IsValidNameData(task.Name)) throw new Exception("The length of your name is invalid");
        if (!this.IsValidDescriptionData(task.Description)) throw new Exception("The length of your description is invalid");
        return true;
    }
    
    private bool IsValidNameData(string name)
    {
        if (name.Length < 3) return false;
        if (name.Length > 20) return false;
        return true;
    }
    
    private bool IsValidDescriptionData(string description)
    {
        if (description.Length < 3) return false;
        if (description.Length > 70) return false;
        return true;
    }
}