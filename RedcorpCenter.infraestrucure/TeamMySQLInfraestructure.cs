using RedcorpCenter.Infra.Context;
using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public class TeamMySQLInfraestructure : ITeamInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public TeamMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext = redcorpCenterDBContext;
        }

        public List<Team> GetAll()
        {
            return _redcorpCenterDBContext.Teams.Where(team => team.IsActive).ToList();
        }

        public Team GetById(int id)
        {
            return _redcorpCenterDBContext.Teams.Find(id);
        }

        public async Task<bool> SaveAsync(Team team)
        {
            try
            {
                _redcorpCenterDBContext.Teams.Add(team);
                await _redcorpCenterDBContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw;
            }
            return true;
        }

        public bool update(int id, Team team)
        {
            Team _team = _redcorpCenterDBContext.Teams.Find(id);
            _team.Name = team.Name;
            _team.Description = team.Description;
            _team.Id_Employee = team.Id_Employee;
            _team.Id_Task = team.Id_Task;
            _team.Id_Project = team.Id_Project;

            _redcorpCenterDBContext.Teams.Update(_team);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }

        public bool delete(int id)
        {
            Team team = _redcorpCenterDBContext.Teams.Find(id);

            team.IsActive = false;

            _redcorpCenterDBContext.Teams.Update(team);

            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
    }
}
