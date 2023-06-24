using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = RedcorpCenter.Infra.Models.Task;
namespace RedcorpCenter.Infra
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
