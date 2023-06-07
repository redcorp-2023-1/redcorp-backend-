using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Domain
{
    public interface ITaskDomain
    {
        public Task<bool> SaveAsync(Task task);
        public bool update(int id, Task task);
        public bool delete(int id);
    }
}

