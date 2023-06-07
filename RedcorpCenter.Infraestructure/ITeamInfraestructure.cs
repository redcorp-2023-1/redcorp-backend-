using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Infraestructure
{
    public interface ITeamInfraestructure
    {
        Team GetById(int id);
        public Task<bool> SaveAsync(Team team);
        public bool update(int id, Team team);
        public bool delete(int id);
        List<Team> GetAll();
    }
}
