using RedcorpCenter.Infraestructure.Models;
using Task = RedcorpCenter.Infraestructure.Models.Task;

namespace RedcorpCenter.Infraestructure
{
    public interface ITeamInfraestructure
    {
        Team GetById(int id);
        public Task<bool> SaveAsync(Team team);
        public bool update(int id, Team team);
        public bool delete(int id);
        List<Team> GetAll();

        List<Task> GetTaskByIdEmploye(int id);
        public List<Team> GetTeamsById(int id);

        public List<Employee> GetEmployeesInSameProject(int employeeId);
    }
}
