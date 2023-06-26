using RedcorpCenter.Infraestructure.Models;
using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Domain
{
    public interface ITeamDomain
    {
        public Task<bool> SaveAsync(Team task);
        public bool update(int id, Team task);
        public bool delete(int id);

        public List<Task> GetTasksByIdEmployee(int employee_id);
    }
}

