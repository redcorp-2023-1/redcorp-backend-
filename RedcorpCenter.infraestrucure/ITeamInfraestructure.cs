using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
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
