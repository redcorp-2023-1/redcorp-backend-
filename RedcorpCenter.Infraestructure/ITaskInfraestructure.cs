using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Infraestructure
{
    public interface ITaskInfraestructure
    {
        Task GetById(int id);
        public Task<bool> SaveAsync(Task task);
        public bool update(int id, Task task);
        public bool delete(int id);
        List<Task> GetAll();
    }
}

