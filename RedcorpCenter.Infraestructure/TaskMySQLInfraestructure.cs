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
        try
        {
            return _redcorpCenterDBContext.Tasks.Where(project => project.IsActive).ToList();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            Console.WriteLine($"Error al recuperar todas las tareas: {ex.Message}");
            throw; // Re-throw the exception to propagate it further
        }
    }

    public Task GetById(int id)
    {
        try
        {
            return _redcorpCenterDBContext.Tasks.Find(id);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            Console.WriteLine($"Error al recuperar la tarea con ID {id}: {ex.Message}");
            throw; // Re-throw the exception to propagate it further
        }
    }

    public async Task<bool> SaveAsync(Task task)
    {
        try
        {
            task.IsActive = true;
            await _redcorpCenterDBContext.Tasks.AddAsync(task);
            await _redcorpCenterDBContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            Console.WriteLine($"Error al guardar la tarea: {ex.Message}");
            throw; // Re-throw the exception to propagate it further
        }
    }

    public bool update(int id, Task task)
    {
        try
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
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            Console.WriteLine($"Error al actualizar la tarea con ID {id}: {ex.Message}");
            throw; // Re-throw the exception to propagate it further
        }
    }

    public bool delete(int id)
    {
        try
        {
            Task task = _redcorpCenterDBContext.Tasks.Find(id);

            task.IsActive = false;

            _redcorpCenterDBContext.Tasks.Update(task);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar la tarea con ID {id}: {ex.Message}");
            throw;
        }
    }
}
